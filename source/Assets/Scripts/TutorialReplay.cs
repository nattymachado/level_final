using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialReplay : MonoBehaviour
{
    [SerializeField] PauseMenuController pauseMenu;

    private void Start()
    {
        bool isReplaying = SaveManager.currentProgress != null && SaveManager.currentProgress.completedTutorial;
       
        pauseMenu.EnableQuitButtonPanel(isReplaying);
    }    
}
