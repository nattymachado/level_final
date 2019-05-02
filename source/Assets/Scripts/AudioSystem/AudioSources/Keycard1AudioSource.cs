using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard1AudioSource : BaseSFXAudioController
{
    //Reference Variables
    [Header("Audio Clip Reference")]
    [SerializeField] protected AudioClip _audioClip;

    //Start
    protected override void Start()
    {
        base.Start();
        GameEvents.RobotSceneAudioEvents.InsertedKeycardBlack1 += PlayClip;
        GameEvents.RobotSceneAudioEvents.InsertedKeycardBlack2 += PlayClip;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvents.RobotSceneAudioEvents.InsertedKeycardBlack1 -= PlayClip;
        GameEvents.RobotSceneAudioEvents.InsertedKeycardBlack2 -= PlayClip;
    }

    private void PlayClip()
    {
        PlayClip(_audioClip, false);
    }
}
