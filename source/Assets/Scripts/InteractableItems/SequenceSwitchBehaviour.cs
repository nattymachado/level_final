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
                transform.Rotate(0, 0, 180);
                GetComponentInParent<SwitchControllerBehaviour>().CheckSwitchPosition(id);
                SetActive(false);
                _isOn = true;
                _isLocked = true;
            }
            
        }

        public void Clear()
        {
            if (_isOn)
            {
                SetActive(false);
                transform.Rotate(0, 0, -180);
                _isOn = false;
                _isLocked = false;
            }
            
        }

    }
}
