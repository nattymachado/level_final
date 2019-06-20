using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
  [SerializeField] private Image background;
  private static LoadingScreen Instance;

  void Awake()
  {
    if (Instance != null)
    {
      DestroyImmediate(this.gameObject);
    }
    else
    {
      Instance = this;
    }
  }

  void OnDestroy()
  {
    if (Instance == this)
    {
      Instance = null;
    }
  }

  public void SetupBackground(Sprite bg){
    background.sprite = bg;
  }

  public void ChangeSceneAfterDuration(float duration, string sceneName)
  {
    StartCoroutine(WaitAndChangeSCene(duration, sceneName));
  }

  private IEnumerator WaitAndChangeSCene(float duration, string sceneName)
  {
    float timer = 0;
    while (timer < duration)
    {
      timer += Time.deltaTime;
      yield return null;
    }
    SceneChanger.Instance.ChangeToScene(sceneName);
  }

  public static void StartPlayLoading(Sprite bg, float duration, string nextScene)
  {
    if (Instance != null)
    {
      Instance.SetupBackground(bg);
      Instance.ChangeSceneAfterDuration(duration, nextScene);
    }
    else
    {
      Debug.LogError("LoadingVideo nao está instanciado");
    }
  }
}