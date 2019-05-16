using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientRecordController : MonoBehaviour
{
    //Control Variables
    [Header("Control Variables")]
    [SerializeField] private string _sceneToLoad;
    public GameEnums.PatientEnum patient;

    //Request Load Scene
    public void RequestLoadScene()
    {
        GetComponentInParent<PatientRecordUIController>().LoadScene(_sceneToLoad);
    }

    //Close Patient Record
    public void RequestClosePatientRecord()
    {
        GetComponentInParent<PatientRecordUIController>().ClosePatientRecord(patient);
    }
}
