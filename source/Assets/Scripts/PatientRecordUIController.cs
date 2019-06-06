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
        foreach (PatientRecordController patientRecord in _patientRecordArray)
        {
            if (patientRecord.patient == requestedPatient)
            {
                patientRecord.gameObject.SetActive(true);
                GameEvents.AudioEvents.TriggerSFX.SafeInvoke("OpenPatientRecord", false, false);
                return;
            }
        }
    }

  //Load Scene
  public void LoadScene(LevelDefs level)
  {
    if (level.LoadingVideo != null)
    {
      SceneChanger.Instance.ChangeToScene(
          "loadingVideo",
          () => { LoadingVideo.StartPlayVideo(level.LoadingVideo, level.LoadingDuration, level.SceneName); },
          null
      );
    }
    else
    {
      SceneChanger.Instance.ChangeToScene(level.SceneName);
    }
  }
}
