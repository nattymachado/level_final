using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatientOnHospitalBehaviour : InteractableItemBehaviour
{
    [SerializeField] public GameEnums.PatientEnum patient;
    [SerializeField] private GameEnums.LevelName levelName;
    [SerializeField] private GameObject _patientModel;
    [SerializeField] private GameObject _zzz;
    [SerializeField] private Transform _endPosition;

    public GameEnums.LevelName LevelName {get {return levelName;}}


    protected override void ExecuteAction(CharacterBehaviour character)
    {
        SetActive(false);
        _zzz.SetActive(false);
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
        GameEvents.UIEvents.OpenPatientRecord.SafeInvoke(patient);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(true);
    }

    private void Start()
    {
        if (SaveManager.GetLevelProgress(levelName) == null )
        {
            SaveManager.CreateProgressFile("player");
        }
        SaveManager.GetLevelProgress(levelName).patientLeftBed = false;
        SaveManager.GetLevelProgress(levelName).levelConcluded = true;
        if (SaveManager.GetLevelProgress(levelName).patientLeftBed == true) {
            SetActive(false);
            _zzz.SetActive(false);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
            _patientModel.SetActive(false);
            return;
        }
        if (!SaveManager.GetLevelProgress(levelName).levelConcluded)
            return;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
        SetActive(false);
        _zzz.SetActive(false);
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


