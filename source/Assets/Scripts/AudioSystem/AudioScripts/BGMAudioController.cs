using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMAudioController : BaseAudioController
{
    //Control Variables
    [Header("Specific Configurations")]
    [SerializeField] private float _volumeFadeFactor;

    //Reference Variables
    [Header("Audio Clip")]
    [SerializeField] private AudioClip _audioClip;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.spatialBlend = 0f;
        _audioSource.Play();
        GameEvents.AudioEvents.SetBGMVolume += SetDesiredVolume;
    }

    //Adjust Volume
    protected override void SetDesiredVolume(float newVolume)
    {
        _audioSource.volume = newVolume * _baseVolumeFactor;
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetBGMVolume -= SetDesiredVolume;
    }
}
