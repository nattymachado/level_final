using UnityEngine;
using System.Collections;

public class CardBox1Behaviour : InteractableItemBehaviour
{
    [SerializeField] CharacterBehaviour character;
    [SerializeField] Animator gateAnimator;
    [SerializeField] Animator gateAnimator2;
    [SerializeField] string cardName;
    [SerializeField] string cardName2;
    private bool _canOpenDoor = false;

    protected override void ExecuteAction(Collider other)
    {
        if (character && character.CheckInventaryObjectOnSelectedPosition(cardName))
        {
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            gateAnimator.SetBool("isOpen", true);
            SetActive(false);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false);
        }
        else if (character && character.CheckInventaryObjectOnSelectedPosition(cardName2))
        {
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            gateAnimator2.SetBool("isOpen", true);
            SetActive(false);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false);
        }

    }

}
