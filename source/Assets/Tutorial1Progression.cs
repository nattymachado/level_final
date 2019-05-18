using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Progression : MonoBehaviour
{
  [SerializeField] private Animator pinchAnimator;
  [SerializeField] private Animator moveAnimator;
  [SerializeField] private Animator useAnimator;
  [SerializeField] private InputController inputController;
  public delegate void StepStart();
  public delegate bool StepCompletion();

  private bool hasPinched;
  private bool hasMoved;
  private bool hasUsed;

  private TutorialStep currentStep;
  private List<TutorialStep> steps = new List<TutorialStep>();


  void OnEnable()
  {
    // registra eventos
    GameEvents.LevelEvents.LevelIntroStarted += StartTutorial;
    GameEvents.LevelEvents.Zoomed += RegisterPinch;
    GameEvents.LevelEvents.Moved += RegisterMove;
    GameEvents.LevelEvents.Used += RegisterUse;
  }
  void OnDisable()
  {
    // registra eventos
    GameEvents.LevelEvents.LevelIntroStarted -= StartTutorial;
    GameEvents.LevelEvents.Zoomed -= RegisterPinch;
    GameEvents.LevelEvents.Moved -= RegisterMove;
    GameEvents.LevelEvents.Used -= RegisterUse;
  }

  void Awake()
  {
    // cria os passos do tutorial
    TutorialStep pinchStep = new TutorialStep(pinchAnimator, new StepStart(PinchStart), new StepCompletion(PinchCompletion));
    TutorialStep moveStep = new TutorialStep(moveAnimator, new StepStart(MoveStart), new StepCompletion(MoveCompletion));
    TutorialStep useStep = new TutorialStep(useAnimator, new StepStart(UseStart), new StepCompletion(UseCompletion));

    steps.Add(pinchStep);
    steps.Add(moveStep);
    steps.Add(useStep);
  }

  void Start()
  {
    inputController.ChangePermissions(false, false, false, false);
  }

  void Update()
  {

    if (currentStep != null)
    {
      if (currentStep.TestCompletion())
      {
        NextStep();
      }
    }
  }

  private void StartTutorial() { NextStep(); }

  private void PinchStart() { inputController.ChangePermissions(true, false, false, false); }
  private bool PinchCompletion() { return hasPinched; }
  private void MoveStart() { inputController.ChangePermissions(true, true, false, false); }
  private bool MoveCompletion() { return hasMoved; }
  private void UseStart() { inputController.ChangePermissions(true, true, false, false); }
  private bool UseCompletion() { return hasUsed; }
  private void RegisterPinch() { hasPinched = true; }
  private void RegisterMove() { hasMoved = true; }
  private void RegisterUse() { hasUsed = true; }


  private void NextStep()
  {
    Debug.Log("ping");
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
        FinishTutorial();
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

  void FinishTutorial()
  {

  }

  private class TutorialStep
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
      animator.SetBool("appear", active);
    }

  }

}


