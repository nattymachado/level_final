using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial2Progression : TutorialProgression
{
  [SerializeField] private Animator swipeAnimator;
  [SerializeField] private Animator pickAnimator;
  [SerializeField] private Animator tapAnimator;
  [SerializeField] private Animator useAnimator;

  private bool hasSwiped;
  private bool hasPicked;
  private bool hasTapped;
  private bool hasUsed;

  protected override void OnEnable()
  {
    base.OnEnable();

    // registra eventos
    GameEvents.LevelEvents.Rotated += RegisterSwipe;
    GameEvents.LevelEvents.PickedItem += RegisterPick;
    GameEvents.LevelEvents.SelectedItem += RegisterTap;
    GameEvents.LevelEvents.UsedItem += RegisterUse;
  }
  protected override void OnDisable()
  {
    base.OnDisable();

    // registra eventos
    GameEvents.LevelEvents.Rotated -= RegisterSwipe;
    GameEvents.LevelEvents.PickedItem -= RegisterPick;
    GameEvents.LevelEvents.SelectedItem -= RegisterTap;
    GameEvents.LevelEvents.UsedItem -= RegisterUse;
  }

  protected override void Awake()
  {
    base.Awake();

    // cria os passos do tutorial
    TutorialStep swipeStep = new TutorialStep(swipeAnimator, new StepStart(SwipeStart), new StepCompletion(SwipeCompletion));
    TutorialStep pickStep = new TutorialStep(pickAnimator, new StepStart(PickStart), new StepCompletion(PickCompletion));
    TutorialStep tapStep = new TutorialStep(tapAnimator, new StepStart(TapStart), new StepCompletion(TapCompletion));
    TutorialStep useStep = new TutorialStep(useAnimator, new StepStart(UseStart), new StepCompletion(UseCompletion));

    steps.Add(swipeStep);
    steps.Add(pickStep);
    steps.Add(tapStep);
    steps.Add(useStep);
    steps.Add(finishStep);
  }

  private void SwipeStart() { inputController.ChangePermissions(true, false, true, false); }
  private bool SwipeCompletion() { return hasSwiped; }
  private void PickStart() { inputController.ChangePermissions(true, true, true, true); }
  private bool PickCompletion() { return hasPicked; }
  private void TapStart() { return; }
  private bool TapCompletion() { return hasTapped; }
  private void UseStart() { return; }
  private bool UseCompletion() { return hasUsed; }


  private void RegisterSwipe() { hasSwiped = true; }
  private void RegisterPick() { hasPicked = true; }
  private void RegisterTap() { hasTapped = true; }
  private void RegisterUse() { hasUsed = true; }

  protected override void Finish()
  {
    base.Finish();

    // muda cena
    SceneManager.LoadScene("hospital");
  }
}
