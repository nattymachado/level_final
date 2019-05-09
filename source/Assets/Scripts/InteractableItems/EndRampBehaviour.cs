using UnityEngine;
using System.Collections;

public class EndRampBehaviour : MonoBehaviour
{
    [SerializeField] Transform initPosition;

    void OnTriggerEnter(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        character.Move(initPosition.position);
    }
}
