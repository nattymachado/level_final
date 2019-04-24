using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
  public void LoadScene(string sceneName)
  {
    if (Application.CanStreamedLevelBeLoaded(sceneName))
    {
      SceneManager.LoadScene(sceneName);
    }
    else
    {
      Debug.LogError("Scene " + sceneName + " does not exists.");
    }
  }

  public void LoadLevelSelector(){
      LoadScene("LevelSelection");
  }
}
