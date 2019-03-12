using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterBehaviour : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public List<string> inventary;
    
    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        /*if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Clicou");
            Move(Input.mousePosition);
        }*/
    }

    public void Move(Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (transform.parent.GetComponent<PlatformBehaviour>() != null)
            {
                PlatformEndPointBehaviour platformEndPointBehaviour = hit.collider.gameObject.GetComponent<PlatformEndPointBehaviour>();
                if (platformEndPointBehaviour)
                {
                    Debug.Log("Cliquei");
                    platformEndPointBehaviour.PlatformBehaviour.ExcludeCharacter(this);
                }
                return;
            }
            
            Debug.Log(hit.collider.gameObject);
            NavMeshAgent.destination = hit.point;
            NavMeshAgent.isStopped = false;
            LadderBehaviour ladderBehaviour = hit.collider.gameObject.GetComponent<LadderBehaviour>();

            if (ladderBehaviour)
            {
                ladderBehaviour.isActiveToMove = true;
                return;
            }

            ButtonBehaviour buttonBehaviour = hit.collider.gameObject.GetComponent<ButtonBehaviour>();

            if (buttonBehaviour)
            {
                buttonBehaviour.isActiveToMove = true;
                Debug.Log("Alavanca");
            }

            PlatformBehaviour platformBehaviour = hit.collider.gameObject.GetComponent<PlatformBehaviour>();

            if (platformBehaviour)
            {
                platformBehaviour.IncludeCharacter(this);
            }

            

            
        }

        
    } 
}
