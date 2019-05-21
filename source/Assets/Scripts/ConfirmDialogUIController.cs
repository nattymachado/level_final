using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmDialogUIController : MonoBehaviour
{
    //Reference Variables
    [Header("Control Variables")]
    [SerializeField] private TutorialConfirmDialogController tutorialConfirmDialog;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.UIEvents.OpenTutorialDialogBox += OpenTutorialDialogBox;
        GameEvents.UIEvents.CloseTutorialDialogBox += CloseTutorialDialogBox;
    }

    //OnDestroy Memory Leak Safeguard
    private void OnDestroy()
    {
        GameEvents.UIEvents.OpenTutorialDialogBox -= OpenTutorialDialogBox;
        GameEvents.UIEvents.CloseTutorialDialogBox -= CloseTutorialDialogBox;
    }

    //Open OpenDialogBox
    private void OpenTutorialDialogBox()
    {
        tutorialConfirmDialog.SetQuestion("Do you want to play the tutorial?");
        tutorialConfirmDialog.gameObject.SetActive(true);
    }

    //Open OpenDialogBox
    private void CloseTutorialDialogBox()
    {
        StartCoroutine(WaitToCloseDialog(0.1f));
    }

    IEnumerator WaitToCloseDialog(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        tutorialConfirmDialog.gameObject.SetActive(false);
    }


}
