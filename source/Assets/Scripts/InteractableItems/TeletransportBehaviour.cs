using UnityEngine;
using System.Collections;
using UnityEngine.AI;

namespace prototypeRobot
{
    public class TeletransportBehaviour : InteractableItemBehaviour
    {
        [SerializeField] Transform endPosition;
        public CharacterBehaviour character;
        private bool _canMove = false;

        protected override void ExecuteAction(Collider other)
        {
            character = other.GetComponent<CharacterBehaviour>();
            if (character && !_canMove)
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
            if (_canMove && character != null)
            {
                _canMove = false;
                character.DisableNavegation();
                character.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                StartCoroutine(WaitToMove());
            }
        }

        private void Move()
        {
            if (!character)
                return;
            character.transform.position = new Vector3(endPosition.position.x, endPosition.position.y + 0.1f, endPosition.position.z);
            character.EnableNavegation();
            character = null;
        }
    }
}
