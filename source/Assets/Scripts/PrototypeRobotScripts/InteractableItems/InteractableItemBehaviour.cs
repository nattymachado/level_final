using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class InteractableItemBehaviour : MonoBehaviour
    {

        private bool _isActive = false;
        [SerializeField] public GridBehaviour Grid;

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggered");
            if (_isActive)
            {
                ExecuteAction(other);
            }
           
        }

        void OnTriggerStay(Collider other)
        {
            Debug.Log("Triggered");
            if (_isActive)
            {
                ExecuteAction(other);
            }
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
        }

        protected virtual void ExecuteAction(Collider other)
        {

        }
    }
}
