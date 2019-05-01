using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioSource : BaseSFXAudioController
{
    //Reference Variables
    [Header("Audio Clip Reference")]
    [SerializeField] protected AudioClip _audioClip;

    //Control Variables
    [Header("Control Variables")]
    [SerializeField] protected bool _playOnAwake;

    //Start
    protected override void Start()
    {
        base.Start();
        _audioSource.clip = _audioClip;
        _audioSource.playOnAwake = _playOnAwake;
        _audioSource.loop = true;
        if(_playOnAwake) _audioSource.Play();
    }
}
