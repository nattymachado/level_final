using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatientOnHospitalBehaviour : InteractableItemBehaviour
{
    [SerializeField] public GameEnums.PatientEnum patient;
    [SerializeField] private GameEnums.LevelEnum _patientLevel;
    [SerializeField] private GameObject _patientModel;
    [SerializeField] private Transform _endPosition;


    protected override void ExecuteAction(Collider other)
    {
        SetActive(false);
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
        GameEvents.UIEvents.OpenPatientRecord.SafeInvoke(patient);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(true);
    }

    private void Start()
    {
        Debug.Log("Checking:" + patient);
        if (GameStatus.Instance.CheckIfPatientIsDeactivated(patient)) {
            Debug.Log("Deactivate:" + patient);
            SetActive(false);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            _patientModel.SetActive(false);
            return;
        }
        if (!_patientModel || GameStatus.Instance.GetLastLevel() != _patientLevel)
            return;
        Debug.Log("Returning:" + patient);
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        SetActive(false);
        _patientModel.SetActive(true);
        _patientModel.GetComponent<Animator>().SetBool("isScared", false);
        _patientModel.GetComponent<Animator>().SetBool("isWalking", true);
        _patientModel.GetComponent<NavMeshAgent>().updateRotation = true;
        StartCoroutine(WaitToGoAway(1.5f));

        
    }

    IEnumerator WaitToGoAway(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _patientModel.GetComponent<NavMeshAgent>().isStopped = false;
        _patientModel.GetComponent<NavMeshAgent>().SetDestination(_endPosition.position);
    }
}


