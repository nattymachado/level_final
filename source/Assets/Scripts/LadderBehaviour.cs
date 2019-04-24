using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LadderActionType
{
    UP,
    DOWN,
    NONE
}
public class LadderBehaviour : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform upPoint;
    [SerializeField] private Vector3 characterRotation;
    [SerializeField] private float speed = 1;
    public bool isActiveToMove = false;
    public CharacterBehaviour character = null;
    public LadderActionType action = LadderActionType.NONE;

    private void Update()
    {
        if (isActiveToMove && character != null)
        {
            if (action == LadderActionType.UP)
            {
                ToEndPoint();
            } else if (action == LadderActionType.DOWN)
            {
                ToStartPoint();
            }
        }
    }

    public void ToEndPoint()
    {
        Debug.Log("To End Point");
        float step = speed * Time.deltaTime;
        Vector3 target = new Vector3(character.transform.position.x, endPoint.position.y, character.transform.position.z);
        action = LadderActionType.UP;
        character.NavMeshAgent.enabled = false;
        character.transform.eulerAngles = characterRotation;
        character.transform.position = new Vector3(endPoint.position.x, character.transform.position.y, endPoint.position.z);
        character.transform.position = Vector3.MoveTowards(character.transform.position, target, step);
        
        
        if (character.transform.position.y == endPoint.position.y)
        {
            isActiveToMove = false;
            character.transform.position = upPoint.position;
            action = LadderActionType.NONE;
            character.NavMeshAgent.enabled = true;
            character = null;
        }
    }

    public void ToStartPoint()
    {
        Debug.Log("To Start Point");
        float step = speed * Time.deltaTime;
        action = LadderActionType.DOWN;
        character.NavMeshAgent.enabled = false;
        character.transform.eulerAngles = characterRotation;
        character.transform.position = new Vector3(startPoint.position.x, character.transform.position.y, startPoint.position.z);
        character.transform.position = Vector3.MoveTowards(character.transform.position, startPoint.position, step);
        if (character.transform.position.y == startPoint.position.y)
        {
            action = LadderActionType.NONE;
            isActiveToMove = false;
            character.NavMeshAgent.enabled = true;
            character = null;
        }
    }


}
