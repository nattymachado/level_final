using UnityEngine;
using System.Collections;

public class Switch1Behaviour : InteractableItemBehaviour
{
    [SerializeField] GameObject gate;
    [SerializeField] float yPosition;
    [SerializeField] float speed;
    private bool _canOpenGate = false;

    protected override void ExecuteAction(Collider other)
    {

        if (!_canOpenGate)
        {
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            if (character)
            {
                //character.Move(transform.position);
                //character.Rotate();
                //character.Stop();
            }
            transform.Rotate(0, 0, -180);
            GetComponentInChildren<Animator>().SetBool("isOn", true);
            _canOpenGate = true;
            _isLocked = true;
            UpdateOutline(false, 0);
        }



    }

    private void Update()
    {
        base.Shine();
        if (_canOpenGate && gate.transform.position.y > yPosition)
        {
            Vector3 target = new Vector3(gate.transform.position.x, yPosition, gate.transform.position.z);
            gate.transform.position = Vector3.MoveTowards(gate.transform.position, target, speed * Time.deltaTime);
        }
        else if (gate.transform.position.y <= yPosition)
        {
            gate.SetActive(false);
        }
    }


}
