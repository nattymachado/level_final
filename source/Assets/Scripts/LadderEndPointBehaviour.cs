using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderEndPointBehaviour : MonoBehaviour
{
    private LadderBehaviour ladderBehaviour;
    private CharacterBehaviour characterBehaviour;

    private void Awake()
    {
        ladderBehaviour = transform.parent.GetComponent<LadderBehaviour>();
    }
    void OnTriggerEnter(Collider other)
    {
        CheckCharacterToStartPoint(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckCharacterToStartPoint(other);

    }

    void CheckCharacterToStartPoint(Collider other)
    {
        characterBehaviour = other.GetComponent<CharacterBehaviour>();
        if (characterBehaviour != null)
        {
            if (ladderBehaviour.isActiveToMove && ladderBehaviour.action == LadderActionType.NONE)
            {
                ladderBehaviour.action = LadderActionType.DOWN;
                ladderBehaviour.character = characterBehaviour;
            }

        }
    }
}
