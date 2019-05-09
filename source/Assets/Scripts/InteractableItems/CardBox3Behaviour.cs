using UnityEngine;
using System.Collections;


public class CardBox3Behaviour : InteractableItemBehaviour
{
    [SerializeField] CharacterBehaviour character;
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] string cardName;

    protected override void ExecuteAction(Collider other)
    {
        if (character && character.CheckInventaryObjectOnSelectedPosition(cardName))
        {
            item1.SetActive(true);
            item2.SetActive(true);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false);
        }

    }


}

