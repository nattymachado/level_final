using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class CardBox2Behaviour : InteractableItemBehaviour
    {
        [SerializeField] CharacterBehaviour character;
        [SerializeField] ColorControllerBehaviour colorController;
        [SerializeField] string cardName;

        protected override void ExecuteAction(Collider other)
        {
            if (character && character.CheckInventaryObjectOnSelectedPosition(cardName))
            {
                GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
                StartCoroutine(WaitToTurnOn(1f));
            }

        }

        IEnumerator WaitToTurnOn(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            TurnOn();
        }

        private void TurnOn()
        {
            colorController.turnOnColors();
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("InsertedKeycard", false, false);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("ComputerBeeps", true, false);
        }


    }
}
