using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatientEndPositionBehaviour : MonoBehaviour
{

    private const string CREDITS_SCENE = "credits";
    [SerializeField] private VictoryHospitalCanvasController victoryCanvas;

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
            OpenVictoryCanvas();
            //SceneChanger.Instance.ChangeToScene(CREDITS_SCENE);
        }
    }

    private void OpenVictoryCanvas()
    {
        // trigger events
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
        victoryCanvas.Open();
        StartCoroutine(WaitToGoToCredits(5f));
    }

    System.Collections.IEnumerator WaitToGoToCredits(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (!victoryCanvas.Clicked)
        {
            SceneChanger.Instance.ChangeToScene(CREDITS_SCENE);
        }
        
    }
}


