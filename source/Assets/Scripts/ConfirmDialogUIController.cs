using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmDialogUIController : MonoBehaviour
{
    //Reference Variables
    [Header("Control Variables")]
    [SerializeField] private TutorialConfirmDialogController tutorialConfirmDialog;
    [SerializeField] private ResetConfirmDialogController resetConfirmDialog;
    [SerializeField] private PauseMenuController _pauseMenuController;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.UIEvents.OpenTutorialDialogBox += OpenTutorialDialogBox;
        GameEvents.UIEvents.CloseTutorialDialogBox += CloseTutorialDialogBox;
        GameEvents.UIEvents.OpenResetDialogBox += OpenResetDialogBox;
        GameEvents.UIEvents.CloseResetDialogBox += CloseResetDialogBox;
    }

    //OnDestroy Memory Leak Safeguard
    private void OnDestroy()
    {
        GameEvents.UIEvents.OpenTutorialDialogBox -= OpenTutorialDialogBox;
        GameEvents.UIEvents.CloseTutorialDialogBox -= CloseTutorialDialogBox;
        GameEvents.UIEvents.OpenResetDialogBox -= OpenResetDialogBox;
        GameEvents.UIEvents.CloseResetDialogBox -= CloseResetDialogBox;
    }

    //Open OpenDialogBox
    private void OpenTutorialDialogBox()
    {
        GameEvents.UIEvents.OpenMenu.SafeInvoke(true);
        tutorialConfirmDialog.gameObject.SetActive(true);
    }

    //Open OpenDialogBox
    private void CloseTutorialDialogBox()
    {
        StartCoroutine(WaitToCloseTutorialDialog(0.1f));
    }

    //Open OpenDialogBox
    private void OpenResetDialogBox()
    {
        GameEvents.UIEvents.OpenMenu.SafeInvoke(true);
        resetConfirmDialog.gameObject.SetActive(true);
    }

    //Open OpenDialogBox
    private void CloseResetDialogBox()
    {
        StartCoroutine(WaitToCloseResetDialog(0.1f));
    }

    IEnumerator WaitToCloseTutorialDialog(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(false);
        tutorialConfirmDialog.gameObject.SetActive(false);
    }

    IEnumerator WaitToCloseResetDialog(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(false);
        resetConfirmDialog.gameObject.SetActive(false);
        _pauseMenuController.OpenClosePauseMenu(true);
    }


}
