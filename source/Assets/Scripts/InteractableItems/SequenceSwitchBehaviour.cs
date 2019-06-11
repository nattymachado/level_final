using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class SequenceSwitchBehaviour : InteractableItemBehaviour
    {
        [SerializeField] public int id;
        private bool _isOn;


        protected override void ExecuteAction(Collider other)
        {
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            if  (character != null && !character.IsStoped())
            {
                return;
            }
        
            if (!_isOn)
            {
                SetActive(false);
                GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
                StartCoroutine(WaitToChangeState(0.5f));
            }
            
        }

        IEnumerator WaitToChangeState(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            transform.parent.transform.Rotate(0, 0, 180);
            PlaySFXSound();
            GetComponentInParent<SwitchControllerBehaviour>().CheckSwitchPosition(id);
            _isOn = true;
            _isLocked = true;

        }

        private void PlaySFXSound()
        {
            if (id == 0) GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Lever2", false, false);
            else if (id == 1) GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Lever3", false, false);
            else if (id == 2) GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Lever4", false, false);
            else if (id == 3) GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Lever5", false, false);
        }

        public void Clear()
        {
            if (_isOn)
            {
                SetActive(false);
                transform.parent.transform.Rotate(0, 0, -180);
                _isOn = false;
                _isLocked = false;
            }
            
        }

    }
}
