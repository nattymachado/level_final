using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAssistantBehaviour : InteractableItemBehaviour
{
    private bool specialItemIsUsed = false;

    protected override void ExecuteAction(CharacterBehaviour character)
    {
        if (character.CheckIfSpecialIsActivated() && !specialItemIsUsed)
        {
            specialItemIsUsed = true;
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            // wait form reach destination
            SpecialCompleteItem specialCompleteItem = character.specialCompleteItem.GetComponent<SpecialCompleteItem>();
            specialCompleteItem.GetComponent<Animator>().SetTrigger("GoToPatient");
            StartCoroutine(WaitForSpacialItemGoToPacient(specialCompleteItem));
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Fanfare", false, false);
        }
    }

    private IEnumerator WaitForSpacialItemGoToPacient(SpecialCompleteItem specialCompleteItem)
    {
        while (specialCompleteItem.specialItemObject.activeSelf)
        {
            yield return null;
        }
    
        SceneChanger.Instance.ChangeToScene("hospital");
    }
}
