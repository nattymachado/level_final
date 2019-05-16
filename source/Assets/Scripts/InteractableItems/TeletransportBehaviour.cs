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

        IEnumerator WaitToMove()
        {
            yield return new WaitForSeconds(1f);
            Move();
        }

        private void Update()
        {
            //base.Shine();
            if (_canMove && _character != null)
            {
                _canMove = false;
                _character.DisableNavegation();
                _character.transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
                StartCoroutine(WaitToMove());
            }
        }

        private void Move()
        {
            if (!_character)
                return;
            _character.transform.position = new Vector3(endPosition.position.x, endPosition.position.y + 0.1f, endPosition.position.z);
            _character.EnableNavegation();
            _character = null;
        }
    }
}
