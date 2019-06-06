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
            GetComponentInParent<SwitchControllerBehaviour>().CheckSwitchPosition(id);
            _isOn = true;
            _isLocked = true;

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
