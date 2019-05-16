using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{


    public void QuitButton()
    {
        SceneManager.LoadScene("start", LoadSceneMode.Single);
    }
}
