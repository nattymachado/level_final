using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace prototypeRobot
{
    public class TeletransportBehaviour : InteractableItemBehaviour
    {
        [SerializeField] Transform endPosition;
        private CharacterBehaviour _character;
        private bool _canMove = false;

        protected override void ExecuteAction(Collider other)
        {
            _character = other.GetComponent<CharacterBehaviour>();
            if (_character && !_canMove)
            {
                
                _canMove = true;
            }
            
        }

        private void Update()
        {
            if (_canMove && _character != null)
            {
                _character.GetComponent<NavMeshAgent>().enabled = false;
                _character.transform.position = endPosition.position;
                _character.GetComponent<NavMeshAgent>().enabled = true;
                _character = null;
            }
        }

    }
}
