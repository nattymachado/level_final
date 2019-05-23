using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    //Reference Variables
    [Header("Required References")]
    [SerializeField] private GameObject pauseMenuGameObject;
    [SerializeField] private GameObject patientReportGameObject;

    public void OpenClosePauseMenu(bool state)
    {
        if (state) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        pauseMenuGameObject.SetActive(state);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(state);
    }

    public void OpenClosePatientReport(bool state)
    {
        patientReportGameObject.SetActive(state);
    }

    public void ExitLevelButton()
    {
        Time.timeScale = 1f;
        SceneChanger.Instance.ChangeToScene("hospital");
        // SceneManager.LoadScene("hospital", LoadSceneMode.Single);
    }
}
