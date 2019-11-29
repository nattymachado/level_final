using UnityEngine;
using System.Collections;

public class EndRampBehaviour : MonoBehaviour
{
    [SerializeField] Transform initPosition;
    [SerializeField] InitRampBehaviour init;
    public bool isActive = false;

    void OnTriggerEnter(Collider other)
    {
        Vector3 toTarget = (other.gameObject.transform.position - transform.position).normalized;
        Debug.Log(Vector3.Dot(toTarget, transform.forward));
        if (Vector3.Dot(toTarget, transform.forward) > 0)
        {
            //Descendo
            Debug.Log("Descendo");
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            character.Move(initPosition.position);
        } else
        {
            Debug.Log("Subindo");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Vector3 toTarget = (other.gameObject.transform.position - transform.position).normalized;
        Debug.Log(Vector3.Dot(toTarget, transform.forward));
        if (Vector3.Dot(toTarget, transform.forward) <= 0)
        {
            //Descendo
            Debug.Log("Descendo");
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            character.Move(initPosition.position);
        }
        else
        {
            Debug.Log("Subindo");
        }
    }
}
