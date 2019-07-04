using UnityEngine;
using System.Collections;

public class EndRampBehaviour : MonoBehaviour
{
    [SerializeField] Transform initPosition;
    [SerializeField] InitRampBehaviour init;
    public bool isActive = false;

    void OnTriggerEnter(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();

        if (!isActive)
            return;
        character.Move(initPosition.position);
    }

    void OnTriggerExit(Collider other)
    {
        if (!isActive && init.isActive)
        {
            isActive = true;
            init.isActive = false;
        }
            
    }
}
