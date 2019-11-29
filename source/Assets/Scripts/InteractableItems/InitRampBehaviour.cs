using UnityEngine;
using System.Collections;

public class InitRampBehaviour : MonoBehaviour
{
    [SerializeField] Transform endPosition;
    [SerializeField] EndRampBehaviour end;
    public bool isActive = false; 

    void OnTriggerEnter(Collider other)
    {

        Vector3 toTarget = (other.gameObject.transform.position - transform.position).normalized;
        if (Vector3.Dot(toTarget, transform.forward) <= 0)
        {
            //Subindo
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            character.Move(endPosition.position);
        }
    }

    void OnTriggerExit(Collider other)
    {

        Vector3 toTarget = (other.gameObject.transform.position - transform.position).normalized;
        if (Vector3.Dot(toTarget, transform.forward) > 0)
        {
            //Subindo
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            character.Move(endPosition.position);
        }
    }

}
