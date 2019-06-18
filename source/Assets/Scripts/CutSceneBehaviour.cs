using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneBehaviour : MonoBehaviour
{
    [SerializeField] private string _nextCutScene;
    [SerializeField] private float _timeToWait = 1f;

    private void Start()
    {
        StartCoroutine(WaitToLoadTheNextScene());
    }


    private IEnumerator WaitToLoadTheNextScene()
    {
        yield return new WaitForSeconds(_timeToWait);
        SceneChanger.Instance.ChangeToScene(_nextCutScene);

    }
}
