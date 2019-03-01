using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderStartPointBehaviour : MonoBehaviour
{
    private LadderBehaviour ladderBehaviour;

    private void Awake()
    {
        ladderBehaviour = transform.parent.GetComponent<LadderBehaviour>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigou");
        CharacterBehaviour characterBehaviour = other.GetComponent<CharacterBehaviour>();
        if (characterBehaviour != null)
        {
            Debug.Log("Vai chamenr");
            ladderBehaviour.ToEndPoint(characterBehaviour);
        }
        
    }
}
