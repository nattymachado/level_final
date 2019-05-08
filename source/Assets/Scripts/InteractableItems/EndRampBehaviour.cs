using UnityEngine;
using System.Collections;

public class EndRampBehaviour : MonoBehaviour
{
    [SerializeField] Transform initPosition;

    void OnTriggerEnter(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (!character.canMove)
        {
            character.canMove = true;
        } else
        {
            
            character.Stop();
            character.Move(initPosition.position);
            character.canMove = false;
        }
        

    }
}
