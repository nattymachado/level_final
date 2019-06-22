using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatientEndPositionBehaviour : MonoBehaviour
{
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

        if (SaveManager.GetLevelProgress(GameEnums.LevelName.Dog).levelConcluded && SaveManager.GetLevelProgress(GameEnums.LevelName.Robot).levelConcluded && SaveManager.GetLevelProgress(GameEnums.LevelName.Night).levelConcluded)
        {
            Debug.Log("Finalizado a fase");
        }
    }
}


