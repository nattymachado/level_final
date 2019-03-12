using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public bool isActiveToMove = false;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform characterPoint;
    [SerializeField] private Transform excludeCharacterPoint;
    [SerializeField] private float speed = 1;
    private bool toDown = true;
    private bool onBottom = false;
    private float period = 0.0f;
    private Transform oldParent;

    public void Update()
    {
        if (isActiveToMove)
        {
            float step = speed * Time.deltaTime;
            if (toDown)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPoint.position, step);
                if ((transform.position == endPoint.position))
                {
                    if (period > 5)
                    {
                        onBottom = false;
                        toDown = false;
                        period = 0;
                    } else
                    {
                        onBottom = true;
                    }

                }
            } else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint.position, step);
                if ((transform.position == startPoint.position) && period > 5) {
                    toDown = true;
                    period = 0;
                }
            }
            period += Time.deltaTime;

        }

    }

    public void ExcludeCharacter(CharacterBehaviour character)
    {

        if (onBottom)
        {

           
            character.transform.position = excludeCharacterPoint.position;
            character.NavMeshAgent.enabled = true;
            character.transform.parent = oldParent;
            
        }
    }

    public void IncludeCharacter(CharacterBehaviour character)
    { 
    
        if (onBottom) {

            character.NavMeshAgent.enabled = false;
            oldParent = character.transform.parent;
            character.transform.parent = this.gameObject.transform;
            character.transform.position = characterPoint.position;
        }
    }

}
