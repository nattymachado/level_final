using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderStartPointBehaviour : MonoBehaviour
{
    private LadderBehaviour ladderBehaviour;
    private CharacterBehaviour characterBehaviour;

    private void Awake()
    {
        
        ladderBehaviour = transform.parent.GetComponent<LadderBehaviour>();
    }
    void OnTriggerEnter(Collider other)
    {
        CheckCharacterToEndPoint(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckCharacterToEndPoint(other);

    }

    void CheckCharacterToEndPoint(Collider other)
    {
        characterBehaviour = other.GetComponent<CharacterBehaviour>();
        if (characterBehaviour != null)
        {
            if (ladderBehaviour.isActiveToMove && ladderBehaviour.action == LadderActionType.NONE)
            {
                ladderBehaviour.action = LadderActionType.UP;
                ladderBehaviour.character = characterBehaviour;
            }

        }

    }
    
}
