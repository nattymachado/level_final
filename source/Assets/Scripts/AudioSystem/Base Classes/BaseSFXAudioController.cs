using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSFXAudioController : BaseAudioController
{
    //Control Variables
    [Header("Specific Configurations")]
    [SerializeField] protected float _maxHearingDistance;

    //Start
    protected override void Start()
    {
        base.Start();
        _audioSource.spatialBlend = 1f;
        _audioSource.minDistance = 0f;
        _audioSource.rolloffMode = AudioRolloffMode.Linear;
        _audioSource.maxDistance = _maxHearingDistance;
        GameEvents.AudioEvents.SetSFXVolume += SetDesiredVolume;
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetSFXVolume -= SetDesiredVolume;
    }

    //Play Clip
    protected void PlayClip(AudioClip clip, bool loop)
    {
        if (clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.loop = loop;
            _audioSource.Play();
        }
    }
}
