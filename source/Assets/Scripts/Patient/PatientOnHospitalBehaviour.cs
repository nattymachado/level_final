using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatientOnHospitalBehaviour : InteractableItemBehaviour
{
    [SerializeField] private GameEnums.PatientEnum _patient;
    [SerializeField] private GameEnums.LevelEnum _patientLevel;
    [SerializeField] private GameObject _patientModel;
    [SerializeField] private Transform _endPosition;


    protected override void ExecuteAction(Collider other)
    {
        SetActive(false);
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
        GameEvents.UIEvents.OpenPatientRecord.SafeInvoke(_patient);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(true);
    }

    private void Start()
    {
        if (!_patientModel || GameStatus.Instance.GetLastLevel() != _patientLevel)
            return;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        SetActive(false);
        _patientModel.SetActive(true);
        _patientModel.GetComponent<Animator>().SetBool("isScared", false);
        _patientModel.GetComponent<Animator>().SetBool("isWalking", true);
        _patientModel.GetComponent<NavMeshAgent>().updateRotation = true;
        _patientModel.GetComponent<NavMeshAgent>().isStopped = false;
        _patientModel.GetComponent<NavMeshAgent>().SetDestination(_endPosition.position);
    }
}


