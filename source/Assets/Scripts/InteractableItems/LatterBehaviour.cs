using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class LatterBehaviour : InteractableItemBehaviour
{

    [SerializeField] private float _speed=2f;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform initPosition;
    [SerializeField] private Transform movePosition;
    private CharacterBehaviour _character;
    private bool _canMove = false;
    private bool toUpOnLatter = true;

    protected override void ExecuteAction(Collider other)
    {
        _character = other.GetComponent<CharacterBehaviour>();
       
        if (_character && !_canMove)
        {
            toUpOnLatter = (_character.transform.position.y < 0);
            _canMove = true;
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
                _canMove = false;
                _character.transform.position = endPosition.position;
                _character.GetComponent<NavMeshAgent>().enabled = true;
            }
        } else
        {
            if (_character && _character.transform.position.y <= initPosition.transform.position.y)
            {
                _canMove = false;
                _character.transform.position = initPosition.position;
                _character.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
        if (_canMove && _character != null)
        {
            _character.GetComponent<NavMeshAgent>().enabled = false;
            _character.transform.position = new Vector3(movePosition.transform.position.x, _character.transform.position.y, movePosition.transform.position.z);
            _character.transform.Translate(new Vector3(0, y, 0) * Time.deltaTime * _speed);
        }
    }

}
