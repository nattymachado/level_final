using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LoadingVideo : MonoBehaviour
{
  [SerializeField] private VideoPlayer player;
  private static LoadingVideo Instance;

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

  public void PlayVideoOnLoop(VideoClip video)
  {
    player.Stop();
    player.clip = video;
    player.isLooping = true;
    player.waitForFirstFrame = true;
    player.Play();
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

  public static void StartPlayVideo(VideoClip video, float duration, string nextScene)
  {
    if (Instance != null)
    {
      Instance.PlayVideoOnLoop(video);
      Instance.ChangeSceneAfterDuration(duration, nextScene);
    }
    else
    {
      Debug.LogError("LoadingVideo nao está instanciado");
    }
  }
}