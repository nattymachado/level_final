using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PatientRecordLoader : MonoBehaviour
{
    //Variables
    [SerializeField] private LevelPatientReportInfo[] _patientReportArray;

    //OnEnable
    private void OnEnable()
    {
        string level = SceneManager.GetActiveScene().name;
        foreach (LevelPatientReportInfo info in _patientReportArray)
        {
            if (info.levelName.Equals(level)) this.GetComponent<Image>().sprite = info.patientReportSprite;
        }
    }
}
