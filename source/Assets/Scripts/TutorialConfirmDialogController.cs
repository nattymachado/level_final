using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialConfirmDialogController : MonoBehaviour
{
    //Control Variables
    [Header("Control Variables")]
    [SerializeField] private string _tutorialScene;
    [SerializeField] private Text _titleText;

    public void SetQuestion(string question)
    {
        _titleText.text = question;
    }

    //Confirm the action
    public void Confirm(bool isYes)
    {
        if (isYes)
        {
            SceneChanger.Instance.ChangeToScene(_tutorialScene);
        } else
        {
            GameEvents.UIEvents.CloseTutorialDialogBox.SafeInvoke();
        }
    }
}
