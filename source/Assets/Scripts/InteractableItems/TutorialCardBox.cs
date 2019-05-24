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
      SetActive(false);
      GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
      StartCoroutine(WaitToOpenDoor(1f));
    }
  }

  IEnumerator WaitToOpenDoor(float seconds)
  {
    yield return new WaitForSeconds(seconds);
    GameEvents.AudioEvents.TriggerSFXOnPosition("InsertedKeycard", this.transform.position);
  }
}
