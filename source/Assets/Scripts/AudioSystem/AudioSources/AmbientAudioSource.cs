using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioSource : BaseSFXAudioController
{
    //Reference Variables
    [Header("Audio Clip Reference")]
    [SerializeField] protected AudioClip _audioClip;

    //Start
    private void OnEnable()
    {
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }
}
