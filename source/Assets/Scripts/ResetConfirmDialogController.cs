using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetConfirmDialogController : MonoBehaviour
{
    //Confirm the action
    public void Confirm(bool isYes)
    {
        if (isYes)
        {
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Click_Light", false, false);
            SaveManager.DeleteProgressFile();
            SceneChanger.Instance.ChangeToScene("start");
        }
        else
        {
            GameEvents.UIEvents.CloseResetDialogBox.SafeInvoke();
        }
    }
}
