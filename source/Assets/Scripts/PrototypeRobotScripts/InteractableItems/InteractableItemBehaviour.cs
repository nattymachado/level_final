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
            if (_isActive)
            {
                ExecuteAction(other);
            }
           
        }

        void OnTriggerStay(Collider other)
        {
            if (_isActive)
            {
                ExecuteAction(other);
            }
        }

        public void SetActive(bool isActive)
        {
            _isActive = isActive;
            if (isActive)
            {
                StartCoroutine(WaitToInactivate());
            }
            
        }

        IEnumerator WaitToInactivate()
        {
            yield return new WaitForSeconds(10);
            _isActive = false;
        }

        protected virtual void ExecuteAction(Collider other)
        {

        }
    }
}
