using UnityEngine;
using System.Collections;

public class InitRampBehaviour : MonoBehaviour
{
    [SerializeField] Transform endPosition;
    public bool isActive = false;

    void OnTriggerEnter(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();

        /*if (!character)
        {
            character.canMove = true;
            return;
        }*/

        if (!isActive)
            return;

        
        /*if (character.canMove) { 
            character.Stop();
            character.Move(endPosition.position);
            character.canMove = false;
        }*/

    }
}
