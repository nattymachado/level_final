using UnityEngine;
using System.Collections;


public class CardBox3Behaviour : InteractableItemBehaviour
{
    [SerializeField] Animator gateAnimator;
    [SerializeField] SphereCollider itemCollider;
    [SerializeField] GameObject card;
    [SerializeField] string cardName;

    protected override void ExecuteAction(CharacterBehaviour character)
    {
        if (character && character.CheckInventaryObjectOnSelectedPosition(cardName))
        {
            GameEvents.AudioEvents.TriggerRobotTransmission.SafeInvoke();
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false, false);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            StartCoroutine(WaitToOpenGateAndActivateItem(1f));
        }

    }

    IEnumerator WaitToOpenGateAndActivateItem(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        OpenGateAndActivateItem();
    }

    private void OpenGateAndActivateItem()
    {
        itemCollider.isTrigger = true;
        StartCoroutine(nameof(DropCardCorroutine));
    }

    private IEnumerator DropCardCorroutine()
    {
        yield return new WaitForSeconds(0.5f);
        card.SetActive(true);
        gateAnimator.SetBool("isOpen", true);
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Drop_Key_Pink", false, false);
    }
}

