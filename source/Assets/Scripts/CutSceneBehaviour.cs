using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneBehaviour : MonoBehaviour
{
    [SerializeField] private string _nextCutScene;
    [SerializeField] private float _timeToWait = 1f;
    public Action beforeFadeout;

    private void Start()
    {
        StartCoroutine(WaitToLoadTheNextScene());
    }

    private IEnumerator WaitToLoadTheNextScene()
    {
        yield return new WaitForSeconds(_timeToWait);
        if(beforeFadeout != null) SceneChanger.Instance.ChangeToScene(_nextCutScene, beforeFadeout, null, null);
        else SceneChanger.Instance.ChangeToScene(_nextCutScene);
    }
}
