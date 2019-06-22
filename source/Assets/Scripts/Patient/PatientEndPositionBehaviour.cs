using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatientEndPositionBehaviour : MonoBehaviour
{

    private const string CREDITS_SCENE = "credits";
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterBehaviour>() == null)
        {
            other.gameObject.SetActive(false);
            GameEnums.LevelName levelName = other.transform.parent.GetComponent<PatientOnHospitalBehaviour>().LevelName;
            LevelProgress levelProgress = SaveManager.GetLevelProgress(levelName);
            levelProgress.patientLeftBed = true;
            SaveManager.SaveProgressFile();
        }

        if (SaveManager.GetLevelProgress(GameEnums.LevelName.Dog).patientLeftBed && SaveManager.GetLevelProgress(GameEnums.LevelName.Robot).patientLeftBed && SaveManager.GetLevelProgress(GameEnums.LevelName.Night).patientLeftBed)
        {
            SaveManager.DeleteProgressFile();
            SceneChanger.Instance.ChangeToScene(CREDITS_SCENE);
        }
    }
}


