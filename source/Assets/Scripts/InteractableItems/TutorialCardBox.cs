using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCardBox : InteractableItemBehaviour
{
    [SerializeField] string cardName;

    protected override void ExecuteAction(CharacterBehaviour character)
    {
        if (character.CheckInventaryObjectOnSelectedPosition(cardName))
        {
            GameEvents.AudioEvents.TriggerSFX("InsertedKeycard", false, false);
            SetActive(false);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            
        }
    }
}
