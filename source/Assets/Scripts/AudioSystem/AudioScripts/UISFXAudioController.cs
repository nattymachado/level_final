using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFXAudioController : BaseAudioController
{
    //Audio Clip References
    [Header("Audio Clip References")]
    [SerializeField] private AudioClipTriggerInfo[] _audioClips;

    //Start
    protected override void Awake()
    {
        base.Awake();
        _audioSource.spatialBlend = 1f;
        _audioSource.loop = true;
        _audioSource.spatialBlend = 0f;
        _audioType = GameEnums.AudioTypeEnum.UISFX;
        GameEvents.AudioEvents.SetSFXVolume += SetDesiredVolume;
        GameEvents.AudioEvents.TriggerSFX += TriggerAudioClip;
    }

    //Adjust Volume
    protected override void SetDesiredVolume(float newVolume)
    {
        _desiredVolume = newVolume;
        GameConfiguration.Instance.SetSFXVolume(_desiredVolume);
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetSFXVolume -= SetDesiredVolume;
        GameEvents.AudioEvents.TriggerSFX -= TriggerAudioClip;
    }

    //Trigger Audio Clip
    public void TriggerAudioClip(string AudioClip, bool loop, bool forcePlay)
    {
        if (_audioClips != null && (!_audioSource.isPlaying || forcePlay))
        {
            foreach (AudioClipTriggerInfo triggerInfo in _audioClips)
            {
                if (triggerInfo.trigger.Equals(AudioClip)) PlayClip(triggerInfo.audioClip, loop);
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
