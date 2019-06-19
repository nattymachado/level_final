using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    //Reference Variables
    [Header("Required References")]
    [SerializeField] private GameObject pauseOverlayGameObject;
    [SerializeField] private GameObject pausePanelGameObject;
    [SerializeField] private GameObject patientReportGameObject;
    [SerializeField] private GameObject raycastBlocker;
    [SerializeField] private ConfigurationMenu configurationPanel;

    //Control Variables
    private bool isOpen = false;

    public void OpenClosePauseMenu(bool state)
    {
        raycastBlocker.SetActive(state);
        isOpen = state;
        if (state) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        pauseOverlayGameObject.SetActive(state);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(state);
    }

    public void Resume()
    {
        raycastBlocker.SetActive(true);
        isOpen = false;
        pauseOverlayGameObject.SetActive(false);
    }

    public void ResetGame()
    {
        OpenClosePauseMenu(false);
        GameEvents.UIEvents.OpenResetDialogBox.SafeInvoke();
    }

    public void OpenClosePauseMenuDynamic()
    {
        if (isOpen)
        {
            configurationPanel.CloseConfiguration();
            OpenClosePatientReport(false);
            OpenClosePauseMenu(false);
        }
        else OpenClosePauseMenu(true);
    }

    public void OpenClosePatientReport(bool state)
    {
        patientReportGameObject.SetActive(state);
        pausePanelGameObject.SetActive(!state);
        if (state) GameEvents.AudioEvents.TriggerSFX.SafeInvoke("OpenPatientRecord", false, false);
    }

    public void ExitLevelButton()
    {
        Time.timeScale = 1f;
        SceneChanger.Instance.ChangeToScene("hospital");
    }

    public void ReturnTitleScreenButton()
    {
        Time.timeScale = 1f;
        SceneChanger.Instance.ChangeToScene("start");
    }
}
