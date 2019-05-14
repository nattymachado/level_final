using UnityEngine;
using System.Collections;


public class CardBox3Behaviour : InteractableItemBehaviour
{
    [SerializeField] CharacterBehaviour character;
    [SerializeField] Animator gateAnimator;
    [SerializeField] GameObject card;
    [SerializeField] string cardName;

    protected override void ExecuteAction(Collider other)
    {
        if (character && character.CheckInventaryObjectOnSelectedPosition(cardName))
        {
            card.SetActive(true);
            gateAnimator.SetBool("isOpen", true);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false);
        }

    }


}

