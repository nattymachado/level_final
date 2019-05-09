using UnityEngine;
using System.Collections;

public class InitRampBehaviour : MonoBehaviour
{
    [SerializeField] Transform endPosition;
    public bool isActive = false;

    void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;

        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (!character.canMove)
        {
            character.canMove = true;
        }
        else
        {
            
            character.Stop();
            character.Move(endPosition.position);
            character.canMove = false;
        }

    }
}
