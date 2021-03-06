﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PatientRecordController : MonoBehaviour
{
    //Control Variables
    [Header("Control Variables")]
    [SerializeField] private LevelDefs levelToLoad;
    public GameEnums.PatientEnum patient;
    private Animator _animator;
    private PatientRecordUIController recordController;

    //Start
    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        recordController = GetComponentInParent<PatientRecordUIController>();
    }

    //Request Load Scene
    public void RequestLoadScene()
    {
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Click_Light", false, false);
        recordController.LoadScene(levelToLoad);
    }

    //OnEnable
    private void OnEnable()
    {
        _animator.SetBool("Opened", true);
    }

    //Close Patient Record
    public void RequestClosePatientRecord()
    {
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("OpenPatientRecord", false, false);
        _animator.SetBool("Opened", false);
        GameEvents.UIEvents.OpenMenu.SafeInvoke(false);
    }
}
