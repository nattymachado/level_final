using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Tutorial1Progression : TutorialProgression
{
  [SerializeField] private Animator pinchAnimator;
  [SerializeField] private Animator panAnimator;
  [SerializeField] private Animator pickAnimator;
  [SerializeField] private Animator useAnimator;
  [SerializeField] private Animator moveAnimator;
  [SerializeField] private TutorialLeverBehaviour levelInteractable;
  [SerializeField] private ParticleSystem finishParticles;
  [SerializeField] private Collider finishCollider;
  [SerializeField] private TutorialFadePanel fadePanel;

  private bool hasPinched;
  private bool hasPanned;
  private bool hasPicked;
  private bool hasUsed;
  private bool hasMoved;

  protected override void OnEnable()
  {
    base.OnEnable();

    // registra eventos
    GameEvents.LevelEvents.Zoomed += RegisterPinch;
    GameEvents.LevelEvents.Panned += RegisterPan;
    GameEvents.LevelEvents.SpecialItemAddedToInventory += RegisterPick;
    GameEvents.LevelEvents.UsedInteractable += RegisterUse;
    GameEvents.LevelEvents.Moved += RegisterMove;
  }
  protected override void OnDisable()
  {
    base.OnDisable();

    // registra eventos
    GameEvents.LevelEvents.Zoomed -= RegisterPinch;
    GameEvents.LevelEvents.Panned += RegisterPan;
    GameEvents.LevelEvents.SpecialItemAddedToInventory -= RegisterPick;
    GameEvents.LevelEvents.UsedInteractable -= RegisterUse;
    GameEvents.LevelEvents.Moved -= RegisterMove;
  }

  protected override void Awake()
  {
    base.Awake();

    // cria os passos do tutorial
    TutorialStep pinchStep = new TutorialStep(pinchAnimator, new StepStart(PinchStart), new StepCompletion(PinchCompletion));
    TutorialStep panStep = new TutorialStep(panAnimator, new StepStart(PanStart), new StepCompletion(PanCompletion));
    TutorialStep pickStep = new TutorialStep(pickAnimator, new StepStart(PickStart), new StepCompletion(PickCompletion));
    TutorialStep showSpecialStep = new TutorialStep(null, new StepStart(ShowSpecialStart), new StepCompletion(ShowSpecialCompletion));
    TutorialStep useStep = new TutorialStep(useAnimator, new StepStart(UseStart), new StepCompletion(UseCompletion));

    steps.Add(pinchStep);
    steps.Add(pickStep);
    steps.Add(showSpecialStep);
    steps.Add(panStep);
    steps.Add(useStep);
    steps.Add(finishStep);
  }

  private void PinchStart() { inputController.ChangePermissions(true, false, false, false); }
  private bool PinchCompletion() { return hasPinched; }
  private void PanStart() { inputController.ChangePermissions(true, false, false, true); }
  private bool PanCompletion() { return hasPanned; }
  private void PickStart() { inputController.ChangePermissions(true, true, false, true);  }
  private bool PickCompletion() { return hasPicked; }
  private void ShowSpecialStart() { fadePanel.ShowSpecials(); }
  private bool ShowSpecialCompletion() { return !fadePanel.IsVisible; }
  private void UseStart()
  {
    inputController.ChangePermissions(true, true, false, true);
    levelInteractable.TurnOn();
  }
  private bool UseCompletion() { return hasUsed; }

  private void RegisterPinch() { hasPinched = true; }
  private void RegisterPan() { hasPanned = true; }
  private void RegisterPick() { hasPicked = true; }
  private void RegisterUse() { hasUsed = true; }
  private void RegisterMove() { hasMoved = true; }

  protected override void FinishStart()
  {
    levelInteractable.TurnOff();
    moveAnimator.SetBool("appear", true);

    StartCoroutine(FinishStartRoutine());
  }

  protected override void Finish()
  {
    base.Finish();

    // muda cena
    SceneChanger.Instance.ChangeToScene("Tutorial2");
  }

  private IEnumerator FinishStartRoutine()
  {
     yield return StartCoroutine(WaitToOpenDoor(0.8f)); 
     finishParticles.Play();
  }

  protected override bool FinishCompletion()
  {
    return finishCollider.bounds.Contains(character.transform.position);
  }

}


