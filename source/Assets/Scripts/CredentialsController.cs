using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CredentialsController : MonoBehaviour
{ 
    [SerializeField] private float WaitSeconds=1f;
    private const string START_SCENE = "start";


    private void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(WaitSeconds);
        SceneChanger.Instance.ChangeToScene(START_SCENE);
    }
}


