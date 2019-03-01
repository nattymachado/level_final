using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderBehaviour : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform startPoint;

    public void ToStartPoint(CharacterBehaviour character)
    {
        character.transform.position = startPoint.position;
    }

    public void ToEndPoint(CharacterBehaviour character)
    {
        Debug.Log("Chamou");
        character.NavMeshAgent.enabled = false;
        character.transform.position = endPoint.position;
        character.NavMeshAgent.enabled = true;
    }


}
