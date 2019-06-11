using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMControllerLoader : MonoBehaviour
{
    //Required References
    [Header("Required References")]
    [SerializeField] private GameObject _BGMAudioSystem;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("BGMAudioSystem") == null) Instantiate(_BGMAudioSystem);
        else GameEvents.GameStateEvents.BGMSceneLoaded.SafeInvoke();
    }
}
