using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderEndPointBehaviour : MonoBehaviour
{
    private LadderBehaviour ladderBehaviour;

    private void Awake()
    {
        ladderBehaviour = transform.parent.GetComponent<LadderBehaviour>();
    }
    void OnTriggerEnter(Collider other)
    {
        CharacterBehaviour characterBehaviour = other.transform.parent.GetComponent<CharacterBehaviour>();
        Debug.Log("Trigou");
        if (characterBehaviour != null)
        {
            Debug.Log("Vai chamenr");
            ladderBehaviour.ToStartPoint(characterBehaviour);
        }

    }
}
