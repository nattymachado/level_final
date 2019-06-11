using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCardBox : InteractableItemBehaviour
{
    [SerializeField] CharacterBehaviour character;
    [SerializeField] string cardName;

    protected override void ExecuteAction(Collider other)
    {
        if (character && character.CheckInventaryObjectOnSelectedPosition(cardName))
        {
            GameEvents.AudioEvents.TriggerSFX("InsertedKeycard", false, false);
            SetActive(false);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            
        }
    }
}
