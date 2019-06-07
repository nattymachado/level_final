using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogLevelPacient : MonoBehaviour
{
  [SerializeField] private Animator pacientAnimator;
  [SerializeField] private Transform pacientDestination;

  private NavMeshAgent agent;
  private Animator walkRoundAnimator;

  void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
    walkRoundAnimator = GetComponent<Animator>();
  }

  public void StopRunning()
  {
    StartCoroutine(ExecuteStopRunning());
  }

  private IEnumerator ExecuteStopRunning()
  {

    // turn off running
    walkRoundAnimator.enabled = false;
    pacientAnimator.SetTrigger("stopRunning");

    yield return new WaitForSeconds(2f);

    // go to destination
    pacientAnimator.SetTrigger("startWalking");
    agent.SetDestination(pacientDestination.position);

    // wait form reach destination
    yield return StartCoroutine(WaitForDestination());

    // face front
    yield return StartCoroutine(FaceFront());

    // turn on waiting
    pacientAnimator.SetTrigger("stopWalking");
  }

  private IEnumerator WaitForDestination()
  {
    bool reached = false;
    while (!reached)
    {
      if (!agent.pathPending)
      {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
          if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
          {
            reached = true;
          }
        }
      }
      yield return null;
    }
  }

  private IEnumerator FaceFront()
  {
    float dist = Mathf.Abs(transform.rotation.eulerAngles.y - pacientDestination.eulerAngles.y);
    float rotationSpeed = 1f;
    while (dist >= 1)
    {
      Quaternion lookRotation = Quaternion.LookRotation(new Vector3(pacientDestination.forward.x, 0, pacientDestination.forward.z));
      transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
      dist = Mathf.Abs(transform.rotation.eulerAngles.y - pacientDestination.eulerAngles.y);
      yield return null;
    }
  }

}
