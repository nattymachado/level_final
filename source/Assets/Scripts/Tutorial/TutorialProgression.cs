using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialProgression : MonoBehaviour
{
  public delegate void StepStart();
  public delegate bool StepCompletion();
  private bool finished;
  private TutorialStep currentStep;
  protected List<TutorialStep> steps = new List<TutorialStep>();
  [SerializeField] protected InputController inputController;
  [SerializeField] private Animator doorAnimator;
  [SerializeField] private ParticleSystem finishParticles;
  [SerializeField] private Collider finishCollider;
  [SerializeField] private CharacterBehaviour character;
  [SerializeField] private NavMeshObstacle doorStopObstacle;
  protected TutorialStep finishStep;

  protected virtual void OnEnable()
  {
    // registra eventos
    GameEvents.LevelEvents.LevelStarted += StartTutorial;
  }
  protected virtual void OnDisable()
  {
    // registra eventos
    GameEvents.LevelEvents.LevelStarted -= StartTutorial;
  }

  protected virtual void Awake()
  {
    finishStep = new TutorialStep(null, new StepStart(FinishStart), new StepCompletion(FinishCompletion));

    inputController.ChangePermissions(false, false, false, false);
  }

  void Update()
  {

    if (currentStep != null && !finished)
    {
      if (currentStep.TestCompletion())
      {
        NextStep();
      }
    }
  }

  private void StartTutorial() { NextStep(); }

  protected virtual void FinishStart()
  {
    doorAnimator.SetTrigger("abrir");
    finishParticles.Play();
    doorStopObstacle.enabled = false;
  }
  private bool FinishCompletion()
  {
    return finishCollider.bounds.Contains(character.transform.position);
  }

  private void NextStep()
  {
    if (currentStep == null)
    {
      currentStep = steps[0];
    }
    else
    {
      // desativa animação passo atual
      currentStep.ActivateAnimator(false);

      // troca o passo
      int index = steps.IndexOf(currentStep);
      if (index == steps.Count - 1)
      {
        Finish();
      }
      else
      {
        currentStep = steps[index + 1];
      }
    }

    // inicia estado
    currentStep.Start();

    // liga animação do próximo passo
    currentStep.ActivateAnimator(true);
  }

  protected virtual void Finish()
  {
    finished = true;
  }

  protected class TutorialStep
  {
    private bool completed = false;
    private Animator animator;
    private StepStart stepStart;
    private StepCompletion stepCompletion;

    public TutorialStep(Animator anim, StepStart stepSt, StepCompletion stepConc)
    {
      animator = anim;
      stepStart = stepSt;
      stepCompletion = stepConc;
    }

    public void Start()
    {
      stepStart.Invoke();
    }
    public bool TestCompletion()
    {
      return stepCompletion.Invoke();
    }

    public void ActivateAnimator(bool active)
    {
      if (animator != null)
        animator.SetBool("appear", active);
    }

  }

}


