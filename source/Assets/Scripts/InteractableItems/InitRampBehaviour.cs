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
        character.Move(endPosition.position);
    }
}
