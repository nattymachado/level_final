using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial2Progression : TutorialProgression
{
  [SerializeField] private Animator swipeAnimator;
  [SerializeField] private Animator pickAnimator;
  [SerializeField] private Animator openInventoryAnimator;
  [SerializeField] private Animator selectItemAnimator;
  [SerializeField] private Animator useAnimator;

  private bool hasSwiped;
  private bool hasPicked;
  private bool hasOpenedInventary;
  private bool hasSelectedItem;
  private bool hasUsed;

  private int openStepIndex;
  private int useStepIndex;

  protected override void OnEnable()
  {
    base.OnEnable();

    // registra eventos
    GameEvents.LevelEvents.Rotated += RegisterSwipe;
    GameEvents.LevelEvents.PickedItem += RegisterPick;
    GameEvents.LevelEvents.OpenedInventory += RegisterOpen;
    GameEvents.LevelEvents.ClosedInventory += ReturnToOpenStep;
    GameEvents.LevelEvents.SelectedItem += RegisterSelect;
    GameEvents.LevelEvents.UsedItem += RegisterUse;
  }
  protected override void OnDisable()
  {
    base.OnDisable();

    // registra eventos
    GameEvents.LevelEvents.Rotated -= RegisterSwipe;
    GameEvents.LevelEvents.PickedItem -= RegisterPick;
    GameEvents.LevelEvents.OpenedInventory -= RegisterOpen;
    GameEvents.LevelEvents.ClosedInventory -= ReturnToOpenStep;
    GameEvents.LevelEvents.SelectedItem -= RegisterSelect;
    GameEvents.LevelEvents.UsedItem -= RegisterUse;
  }

  protected override void Awake()
  {
    base.Awake();

    // cria os passos do tutorial
    TutorialStep swipeStep = new TutorialStep(swipeAnimator, new StepStart(SwipeStart), new StepCompletion(SwipeCompletion));
    TutorialStep pickStep = new TutorialStep(pickAnimator, new StepStart(PickStart), new StepCompletion(PickCompletion));
    TutorialStep openInventoryStep = new TutorialStep(openInventoryAnimator, new StepStart(OpenInventoryStart), new StepCompletion(OpenInventoryCompletion));
    TutorialStep selectItemStep = new TutorialStep(selectItemAnimator, new StepStart(SelectItemStart), new StepCompletion(SelectItemCompletion));
    TutorialStep useStep = new TutorialStep(useAnimator, new StepStart(UseStart), new StepCompletion(UseCompletion));

    steps.Add(swipeStep);
    steps.Add(pickStep);
    steps.Add(openInventoryStep);
    steps.Add(selectItemStep);
    steps.Add(useStep);
    steps.Add(finishStep);

    openStepIndex = steps.IndexOf(openInventoryStep);
    useStepIndex = steps.IndexOf(useStep);
  }

  private void SwipeStart() { inputController.ChangePermissions(true, false, true, false); }
  private bool SwipeCompletion() { return hasSwiped; }
  private void PickStart() { inputController.ChangePermissions(true, true, true, true); }
  private bool PickCompletion() { return hasPicked; }
  private void OpenInventoryStart() { return; }
  private bool OpenInventoryCompletion() { return hasOpenedInventary; }
  private void SelectItemStart() { return; }
  private bool SelectItemCompletion() { return hasSelectedItem; }
  private void UseStart() { return; }
  private bool UseCompletion() { return hasUsed; }
  private void RegisterSwipe() { hasSwiped = true; }
  private void RegisterPick() { hasPicked = true; }
  private void RegisterOpen() { hasOpenedInventary = true; }
  private void RegisterSelect() { hasSelectedItem = true; }
  private void RegisterUse() { hasUsed = true; }


  private void ReturnToOpenStep()
  {
    if (steps.IndexOf(currentStep) < useStepIndex)
    {
      ChangeToStep(openStepIndex);
      hasOpenedInventary = false;
    }
  }

  protected override void Finish()
  {
    base.Finish();

    // muda cena
    SceneChanger.Instance.ChangeToScene("hospital");
  }
}
