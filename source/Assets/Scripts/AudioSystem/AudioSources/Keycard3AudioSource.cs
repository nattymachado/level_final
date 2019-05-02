using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard3AudioSource : BaseSFXAudioController
{
    //Reference Variables
    [Header("Audio Clip Reference")]
    [SerializeField] protected AudioClip _audioClip;

    //Start
    protected override void Start()
    {
        base.Start();
        GameEvents.RobotSceneAudioEvents.InsertedKeycardRed += PlayClip;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvents.RobotSceneAudioEvents.InsertedKeycardRed -= PlayClip;
    }

    private void PlayClip()
    {
        PlayClip(_audioClip, false);
    }
}
