using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer2AudioSource : BaseSFXAudioController
{
    //Reference Variables
    [Header("Audio Clip Reference")]
    [SerializeField] protected AudioClip _audioClip;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameEvents.RobotSceneAudioEvents.InsertedKeycardGreen += PlayComputer2Ambient;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvents.RobotSceneAudioEvents.InsertedKeycardGreen -= PlayComputer2Ambient;
    }

    private void PlayComputer2Ambient()
    {
        PlayClip(_audioClip, true);
    }
}
