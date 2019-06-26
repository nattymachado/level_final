using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial2Progression : TutorialProgression
{
  [SerializeField] private Animator swipeAnimator;
  [SerializeField] private Animator pickAnimator1;
  [SerializeField] private Animator openInventoryAnimator;
  [SerializeField] private Animator selectItemAnimator;
  [SerializeField] private Animator useAnimator1;
  [SerializeField] private Animator pickAnimator2;
  [SerializeField] private Animator useAnimator2;
  [SerializeField] private Image firstSpecialImage;
  [SerializeField] private TutorialFadePanel fadePanel;

  private bool hasSwiped;
  private bool hasPicked;
  private bool hasOpenedInventary;
  private bool hasSelectedItem;
  private bool hasUsed;
  private bool hasPickedSpecial;
  private bool hasDelivered;

  private int openStepIndex;
  private int useStepIndex;

  protected override void OnEnable()
  {
    base.OnEnable();

    // registra eventos
    GameEvents.LevelEvents.Rotated += RegisterSwipe;
    GameEvents.LevelEvents.ItemAddedToInventory += RegisterPick;
    GameEvents.LevelEvents.OpenedInventory += RegisterOpen;
    GameEvents.LevelEvents.ClosedInventory += ReturnToOpenStep;
    GameEvents.LevelEvents.SelectedItem += RegisterSelect;
    GameEvents.LevelEvents.UsedItem += RegisterUse;
    GameEvents.LevelEvents.SpecialItemAddedToInventory += RegisterPickSpecial;
    GameEvents.GameStateEvents.LevelCompleted += RegisterDeliver;
  }
  protected override void OnDisable()
  {
    base.OnDisable();

    // registra eventos
    GameEvents.LevelEvents.Rotated -= RegisterSwipe;
    GameEvents.LevelEvents.ItemAddedToInventory -= RegisterPick;
    GameEvents.LevelEvents.OpenedInventory -= RegisterOpen;
    GameEvents.LevelEvents.ClosedInventory -= ReturnToOpenStep;
    GameEvents.LevelEvents.SelectedItem -= RegisterSelect;
    GameEvents.LevelEvents.UsedItem -= RegisterUse;
    GameEvents.LevelEvents.SpecialItemAddedToInventory -= RegisterPickSpecial;
    GameEvents.GameStateEvents.LevelCompleted -= RegisterDeliver;
  }

  protected override void Awake()
  {
    base.Awake();

    // cria os passos do tutorial
    TutorialStep swipeStep = new TutorialStep(swipeAnimator, new StepStart(SwipeStart), new StepCompletion(SwipeCompletion));
    TutorialStep pickStep = new TutorialStep(pickAnimator1, new StepStart(PickStart), new StepCompletion(PickCompletion));
    TutorialStep showItensStep = new TutorialStep(null, new StepStart(ShowItensStart), new StepCompletion(ShowItensCompletion));
    TutorialStep openInventoryStep = new TutorialStep(openInventoryAnimator, new StepStart(OpenInventoryStart), new StepCompletion(OpenInventoryCompletion),1f);
    TutorialStep selectItemStep = new TutorialStep(selectItemAnimator, new StepStart(SelectItemStart), new StepCompletion(SelectItemCompletion));
    TutorialStep useStep = new TutorialStep(useAnimator1, new StepStart(UseStart), new StepCompletion(UseCompletion));
    TutorialStep pickSpecialStep = new TutorialStep(pickAnimator2, new StepStart(PickSpecialStart), new StepCompletion(PickSpecialCompletion));

    steps.Add(swipeStep);
    steps.Add(pickStep);
    steps.Add(showItensStep);
    steps.Add(openInventoryStep);
    steps.Add(selectItemStep);
    steps.Add(useStep);
    steps.Add(pickSpecialStep);
    steps.Add(finishStep);

    openStepIndex = steps.IndexOf(openInventoryStep);
    useStepIndex = steps.IndexOf(useStep);

        
  }

  private void Start()
  {
      // put first item on special itens
      CollectibleInventoryController.Instance.AddItemNoAnimation(firstSpecialImage.sprite);
  }

  private void SwipeStart() { inputController.ChangePermissions(true, false, true, false); }
  private bool SwipeCompletion() { return hasSwiped; }
  private void PickStart() { inputController.ChangePermissions(true, true, true, true); }
  private bool PickCompletion() { return hasPicked; }
  private void ShowItensStart() { fadePanel.ShowItens(); }
  private bool ShowItensCompletion() { return !fadePanel.IsVisible; }
  private void OpenInventoryStart() { inventary.EnableDisable(true); }
  private bool OpenInventoryCompletion() { return hasOpenedInventary; }
  private void SelectItemStart() { return; }
  private bool SelectItemCompletion() { return hasSelectedItem; }
  private void UseStart() { return; }
  private bool UseCompletion() { return hasUsed; }
  private void PickSpecialStart(){
    StartCoroutine(WaitToOpenDoor(0.8f)); 
  }
  private bool PickSpecialCompletion() { return hasPickedSpecial; }


  private void RegisterSwipe() { hasSwiped = true; }
  private void RegisterPick() { hasPicked = true; }
  private void RegisterOpen() { hasOpenedInventary = true; }
  private void RegisterSelect() { hasSelectedItem = true; }
  private void RegisterUse() { hasUsed = true; }
  private void RegisterPickSpecial() { hasPickedSpecial = true; }
  private void RegisterDeliver() { hasDelivered = true; }


  private void ReturnToOpenStep()
  {
    if (steps.IndexOf(currentStep) < useStepIndex)
    {
      ChangeToStep(openStepIndex);
      hasOpenedInventary = false;
    }
  }

  protected override void FinishStart()
  {
    useAnimator2.SetBool("appear", true);
  }

  protected override bool FinishCompletion()
  {
    return hasDelivered;
  }

  protected override void Finish()
  {
    base.Finish();

    // muda cena
    SceneChanger.Instance.ChangeToScene("hospital");
  }
}
