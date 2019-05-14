using UnityEngine;
using System.Collections;

public class InitRampBehaviour : MonoBehaviour
{
    [SerializeField] Transform endPosition;
    [SerializeField] EndRampBehaviour end;
    public bool isActive = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entrei ...");
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();

        if (!isActive)
            return;
        character.Move(endPosition.position); 
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("SAi ...");
        if (!isActive && end.isActive)
        {
            isActive = true;
            end.isActive = false;
        }
    }
    
}
