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
  protected TutorialStep currentStep;
  protected List<TutorialStep> steps = new List<TutorialStep>();
  [SerializeField] protected InputController inputController;
  [SerializeField] private Animator doorAnimator;

  [SerializeField] protected CharacterBehaviour character;
  [SerializeField] private NavMeshObstacle doorStopObstacle;
  [SerializeField] protected InventoryCenterBehaviour inventary;
  protected TutorialStep finishStep;
  private Coroutine delay;

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

  protected virtual void Start()
  {
    inventary.EnableDisable(false);
    inventary.Fade(true);
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

  protected virtual void FinishStart(){}

  protected IEnumerator WaitToOpenDoor(float seconds)
  {
      yield return new WaitForSeconds(seconds);
      doorAnimator.SetTrigger("abrir");
      doorStopObstacle.enabled = false;
  }

  protected virtual bool FinishCompletion(){
    return false;
  }

  protected void ChangeToStep(int index)
  {
    currentStep.ActivateAnimator(false);
    currentStep = steps[index];

    StartCurrentStep();
  }

  private void NextStep()
  {
    if (currentStep == null)
    {
      currentStep = steps[0];
    }
    else
    {
      // cancela corotina de delay de animação
      if(delay!=null)
        StopCoroutine(delay);

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

    StartCurrentStep();
  }

  private void StartCurrentStep()
  {
    // inicia estado
    currentStep.Start();

    // liga animação do próximo passo
    if(currentStep.AnimatorDelay>0f)
    {
      delay = StartCoroutine(WaitToActivateAnimator(currentStep.AnimatorDelay));
    } else {
      currentStep.ActivateAnimator(true);
    }
  }


  private IEnumerator WaitToActivateAnimator(float animatorDelay)
  {
    float timer = 0f;
    while (timer < animatorDelay)
    {
      timer += Time.deltaTime;
      yield return null;
    }

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
    private float animatorDelay = 0f;
    private StepStart stepStart;
    private StepCompletion stepCompletion;
    public float AnimatorDelay { get => animatorDelay;}

    public TutorialStep(Animator anim, StepStart stepSt, StepCompletion stepConc, float animDelay = 0f)
    {
      animator = anim;
      stepStart = stepSt;
      stepCompletion = stepConc;
      animatorDelay = animDelay;
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
      {
        animator.SetBool("appear", active);
      }
    }
  }
}


