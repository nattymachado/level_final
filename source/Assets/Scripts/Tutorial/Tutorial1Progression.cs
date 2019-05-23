using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Tutorial1Progression : TutorialProgression
{
  [SerializeField] private Animator pinchAnimator;
  [SerializeField] private Animator moveAnimator;
  [SerializeField] private Animator useAnimator;
  [SerializeField] private TutorialLeverBehaviour levelInteractable;
  private bool hasPinched;
  private bool hasMoved;
  private bool hasUsed;

  protected override void OnEnable()
  {
    base.OnEnable();

    // registra eventos
    GameEvents.LevelEvents.Zoomed += RegisterPinch;
    GameEvents.LevelEvents.Moved += RegisterMove;
    GameEvents.LevelEvents.UsedInteractable += RegisterUse;
  }
  protected override void OnDisable()
  {
    base.OnDisable();

    // registra eventos
    GameEvents.LevelEvents.Zoomed -= RegisterPinch;
    GameEvents.LevelEvents.Moved -= RegisterMove;
    GameEvents.LevelEvents.UsedInteractable -= RegisterUse;
  }

  protected override void Awake()
  {
    base.Awake();

    // cria os passos do tutorial
    TutorialStep pinchStep = new TutorialStep(pinchAnimator, new StepStart(PinchStart), new StepCompletion(PinchCompletion));
    TutorialStep moveStep = new TutorialStep(moveAnimator, new StepStart(MoveStart), new StepCompletion(MoveCompletion));
    TutorialStep useStep = new TutorialStep(useAnimator, new StepStart(UseStart), new StepCompletion(UseCompletion));

    steps.Add(pinchStep);
    steps.Add(moveStep);
    steps.Add(useStep);
    steps.Add(finishStep);
  }

  private void PinchStart() { inputController.ChangePermissions(true, false, false, false); }
  private bool PinchCompletion() { return hasPinched; }
  private void MoveStart() { inputController.ChangePermissions(true, true, false, false); }
  private bool MoveCompletion() { return hasMoved; }
  private void UseStart()
  {
    inputController.ChangePermissions(true, true, false, false);
    levelInteractable.TurnParticlesOn();
  }
  private bool UseCompletion() { return hasUsed; }
  private void RegisterPinch() { hasPinched = true; }
  private void RegisterMove() { hasMoved = true; }
  private void RegisterUse() { hasUsed = true; }

  protected override void FinishStart()
  {
    base.FinishStart();

    levelInteractable.TurnParticlesOff();
  }

  protected override void Finish()
  {
    base.Finish();

    // muda cena
    SceneChanger.Instance.ChangeToScene("Tutorial2");
    // SceneManager.LoadScene("Tutorial2");
  }
}


