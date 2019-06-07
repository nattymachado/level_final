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
            yield return new WaitForSeconds(0.5f);
            Move();
        }

        private void Update()
        {
            //base.Shine();
            if (_canMove && character != null)
            {
                character.DisableNavegation();
                character.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.EnterOnPortal);
                StartCoroutine(WaitToMove());
            }
        }

        private void Move()
        {
            _canMove = false;
            if (!character)
                return;
            character.transform.position = new Vector3(endPosition.position.x, endPosition.position.y + 0.1f, endPosition.position.z);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.EnterOnPortal);
            character.EnableNavegation();
            character = null;
        }
    }
}
