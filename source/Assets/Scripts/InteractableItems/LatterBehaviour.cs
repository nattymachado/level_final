using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class LatterBehaviour : InteractableItemBehaviour
{

    [SerializeField] private float _speed=2f;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform initPosition;
    [SerializeField] private Transform movePosition;
    [SerializeField] private Transform rotationToUpPoint;
    private CharacterBehaviour _character;
    private bool _canMove = false;
    private bool toUpOnLatter = true;
    private bool started;

    protected override void ExecuteAction(Collider other)
    {
        _character = other.GetComponent<CharacterBehaviour>();
       
        if (_character && !_canMove)
        {
            toUpOnLatter = (_character.transform.position.y < endPosition.position.y -1);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.UseLadder);
            _canMove = true;
            started = false;
        }

    }

    private void Update()
    {
        //base.Shine();
        if (!_canMove)
        {
            return;
        }
        float y = -1;
      
        if (toUpOnLatter)
        {
            y = 1;
            if (_character && _character.transform.position.y >= endPosition.transform.position.y)
            {
                GameEvents.FSMEvents.FinishedInteraction.SafeInvoke();
                _canMove = false;
                _character.transform.position = endPosition.position;
                _character.EnableNavegation();
                
                
            }
        } else
        {
            if (_character && _character.transform.position.y <= initPosition.transform.position.y)
            {
                GameEvents.FSMEvents.FinishedInteraction.SafeInvoke();
                _canMove = false;
                _character.transform.position = initPosition.position;
                _character.EnableNavegation();
                
            }
        }

        if (_canMove && _character != null)
        {
            if (!started)
            {
                _character.DisableNavegation();
                _character.transform.position = new Vector3(movePosition.transform.position.x, _character.transform.position.y, movePosition.transform.position.z);
               Vector3 lookAtPosition = rotationToUpPoint.position;
               lookAtPosition.y = _character.transform.position.y;
               _character.transform.LookAt(lookAtPosition);
               
                
                started = true;
            }
            
            _character.transform.Translate(new Vector3(0, y, 0) * Time.deltaTime * _speed);
        }
    }

}
