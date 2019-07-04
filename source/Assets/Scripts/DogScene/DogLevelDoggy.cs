using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogLevelDoggy : MonoBehaviour
{

    [SerializeField] private DogLevelDoor yellowDoor;
    [SerializeField] private DogLevelDoor blueDoor;
    [SerializeField] private Transform bowlDestination;
    [SerializeField] private InventoryObjectBehaviour specialItemDoggy;
    [SerializeField] private Animator doggyAnimator;
    [SerializeField] private DogLevelPacient pacient;
    private NavMeshAgent agent;
    private Animator walkRoundAnimator;

    public bool on = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        walkRoundAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (on)
        {
            on = false;
            GoToBowl();
        }
    }
    public void GoToBowl()
    {
        StartCoroutine(ExecuteGoToBowl());
    }

    private IEnumerator ExecuteGoToBowl()
    {

        // force yellow door open
        yellowDoor.ForceOpen();

        // force blue door open
        blueDoor.ForceClose();

        // turn off running
        walkRoundAnimator.enabled = false;
        doggyAnimator.SetTrigger("stopRunning");

        yield return new WaitForSeconds(1.5f);

        // make pacient stop
        pacient.StopRunning();

        // turn on walking
        doggyAnimator.SetTrigger("startWalking");

        // set destination
        agent.SetDestination(bowlDestination.position);

        // wait form reach destination
        yield return StartCoroutine(WaitForDestination());

        // turn on eating
        doggyAnimator.SetTrigger("stopWalking");

        yield return new WaitForSeconds(2.5f);

        // make special item appear
        specialItemDoggy.gameObject.SetActive(true);

        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("ItemAppear", false, true);

        // deactivate dog
        gameObject.SetActive(false);
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
                        transform.LookAt(bowlDestination);
                        reached = true;
                    }
                }
            }
            yield return null;
        }
    }
}