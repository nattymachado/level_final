using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatientRecordUIController : MonoBehaviour
{
    //Reference Variables
    [Header("Control Variables")]
    [SerializeField] private PatientRecordController[] _patientRecordArray;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.UIEvents.OpenPatientRecord += OpenPatientRecord;
    }

    //OnDestroy Memory Leak Safeguard
    private void OnDestroy()
    {
        GameEvents.UIEvents.OpenPatientRecord -= OpenPatientRecord;
    }

    //Open Patient Record
    private void OpenPatientRecord(GameEnums.PatientEnum requestedPatient)
    {
        foreach(PatientRecordController patientRecord in _patientRecordArray)
        {
            if (patientRecord.patient == requestedPatient)
            {
                patientRecord.gameObject.SetActive(true);
                return;
            }
        }
    }

    //Close Patient Record
    public void ClosePatientRecord(GameEnums.PatientEnum requestedPatient)
    {
        foreach (PatientRecordController patientRecord in _patientRecordArray)
        {
            if (patientRecord.patient == requestedPatient)
            {
                patientRecord.gameObject.SetActive(false);
                return;
            }
        }
    }

    //Load Scene
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
