using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PatientRecordController : MonoBehaviour
{
    //Control Variables
    [Header("Control Variables")]
    [SerializeField] private string _sceneToLoad;
    [SerializeField] private VideoClip _loadingVideo;
    public GameEnums.PatientEnum patient;
    private Animator _animator;

    //Start
    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
    }

    //Request Load Scene
    public void RequestLoadScene()
    {
        GetComponentInParent<PatientRecordUIController>().LoadScene(_sceneToLoad);
    }

    //OnEnable
    private void OnEnable()
    {
        _animator.SetBool("Opened", true);
    }

    //Close Patient Record
    public void RequestClosePatientRecord()
    {
        _animator.SetBool("Opened", false);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(false);
    }
}
