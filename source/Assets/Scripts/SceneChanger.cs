using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : Singleton<SceneChanger>
{
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 2f;
    [SerializeField] private CanvasGroup canvasGroup;
    private AsyncOperation async;
    private Scene currentScene;

    public void ChangeToScene(string sceneName)
    {
        StartCoroutine(SceneChange(sceneName, null, null, null));
    }

    public void ChangeToScene(string sceneName, Action callback)
    {
        StartCoroutine(SceneChange(sceneName, null, null, callback));
    }

    public void ChangeToScene(string sceneName, Action beforeFadeOut, Action beforeFadeIn, Action afterFadeIn)
    {
        StartCoroutine(SceneChange(sceneName, beforeFadeOut, beforeFadeIn, afterFadeIn));
    }

    IEnumerator SceneChange(string nextSceneName, Action beforeFadeOut, Action beforeFadeIn, Action afterFadeIn)
    {
        currentScene = SceneManager.GetActiveScene();

        // execute action
        if (beforeFadeOut != null) beforeFadeOut.Invoke();

        // fade
        yield return StartCoroutine(FadeOut());

        // load scene
        async = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
        while (!async.isDone)
        {
            yield return null;
        }

        // execute action
        if (beforeFadeIn != null) beforeFadeIn.Invoke();

        // fade 
        yield return StartCoroutine(FadeIn());

        // execute callback
        if (afterFadeIn != null) afterFadeIn.Invoke();

        }

        IEnumerator FadeIn()
        {
        canvasGroup.alpha = 1;
        float timer = 0;
        while (timer < fadeInDuration)
        {
            canvasGroup.alpha = 1 - timer / fadeInDuration;
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
        }

        IEnumerator FadeOut()
        {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        float timer = 0;
        while (timer < fadeOutDuration)
        {
            canvasGroup.alpha = timer / fadeOutDuration;
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}
