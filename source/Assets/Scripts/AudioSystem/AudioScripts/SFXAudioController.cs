using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAudioController : BaseAudioController
{
    //Control Variables
    [Header("Specific Configurations")]
    [SerializeField] protected float _minHearingDistance;
    [SerializeField] protected float _maxHearingDistance;

    //Audio Clip References
    [Header("Audio Clip References")]
    [SerializeField] private AudioClipTriggerInfo[] _audioClips;
    [Header("Random Audio Clip References")]
    [SerializeField] private AudioClipArrayTriggerInfo[] _randomAudioClips;

    //Start
    protected override void Awake()
    {
        base.Awake();
        _audioSource.spatialBlend = 1f;
        _audioSource.minDistance = _minHearingDistance;
        _audioSource.maxDistance = _maxHearingDistance;
        _audioSource.rolloffMode = AudioRolloffMode.Linear;  
        GameEvents.AudioEvents.SetSFXVolume += SetDesiredVolume;
        GameEvents.AudioEvents.TriggerSFX += TriggerAudioClip;
        GameEvents.AudioEvents.TriggerRandomSFX += TriggerRandomAudioClip;
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetSFXVolume -= SetDesiredVolume;
        GameEvents.AudioEvents.TriggerSFX -= TriggerAudioClip;
        GameEvents.AudioEvents.TriggerRandomSFX -= TriggerRandomAudioClip;
    }

    //Trigger Audio Clip
    public void TriggerAudioClip(string AudioClip, bool loop)
    {
        if(_audioClips != null)
        {
            foreach (AudioClipTriggerInfo triggerInfo in _audioClips)
            {
                if(triggerInfo.trigger.Equals(AudioClip)) PlayClip(triggerInfo.audioClip, loop);
            }
        }
    }

    //Trigger Random Audio Clip
    public void TriggerRandomAudioClip(string AudioClip, bool loop)
    {
        if (_randomAudioClips != null)
        {
            foreach (AudioClipArrayTriggerInfo triggerInfo in _randomAudioClips)
            {
                if (triggerInfo.trigger.Equals(AudioClip)) PlayClip(triggerInfo.audioClips[Random.Range(0, triggerInfo.audioClips.Length)], loop);
            }
        }
    }

    //Play Clip
    private void PlayClip(AudioClip clip, bool loop)
    {
        if (clip != null)
        {
            _audioSource.clip = clip;
            _audioSource.loop = loop;
            _audioSource.Play();
        }
    }
}
