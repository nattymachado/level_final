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
        _audioType = GameEnums.AudioTypeEnum.SFX;
        GameEvents.AudioEvents.SetSFXVolume += SetDesiredVolume;
        GameEvents.AudioEvents.TriggerSFX += TriggerAudioClip;
        GameEvents.AudioEvents.TriggerRandomSFX += TriggerRandomAudioClip;
        GameEvents.AudioEvents.TriggerSFXOnPosition += TriggerAudioClipOnPosition;
    }

    //Adjust Volume
    protected override void SetDesiredVolume(float newVolume)
    {
        /*if(newVolume > _baseVolume) _desiredVolume = Mathf.Min(_baseVolume + (newVolume - _baseVolume), 1f);
        else _desiredVolume = Mathf.Max(_baseVolume + (_baseVolume - newVolume), 0f);*/
        _desiredVolume = newVolume;
        GameConfiguration.Instance.SetSFXVolume(_desiredVolume);
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetSFXVolume -= SetDesiredVolume;
        GameEvents.AudioEvents.TriggerSFX -= TriggerAudioClip;
        GameEvents.AudioEvents.TriggerRandomSFX -= TriggerRandomAudioClip;
    }

    //Trigger Audio Clip
    public void TriggerAudioClipOnPosition(string audioClip, Vector3 position)
    {
        foreach (AudioClipTriggerInfo triggerInfo in _audioClips)
        {
            if (triggerInfo.trigger.Equals(audioClip)) PlayClipOnPosition(triggerInfo.audioClip, position); ;

        }
       
    }

    //Trigger Audio Clip
    public void TriggerAudioClip(string AudioClip, bool loop, bool forcePlay)
    {
        if(_audioClips != null && (!_audioSource.isPlaying || forcePlay))
        {
            foreach (AudioClipTriggerInfo triggerInfo in _audioClips)
            {
                if (triggerInfo.trigger.Equals(AudioClip)) PlayClip(triggerInfo.audioClip, loop);
                    
            }
        }
    }

    //Trigger Random Audio Clip
    public void TriggerRandomAudioClip(string AudioClip, bool loop, bool forcePlay)
    {
        if (_randomAudioClips != null && (!_audioSource.isPlaying || forcePlay))
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

    //Play Clip
    private void PlayClipOnPosition(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, _desiredVolume);
        }
    }

    //Update
    protected virtual void Update()
    {
        /*if (_audioSource.volume != _desiredVolume)
        {
            Debug.Log("Upadting:" +  _desiredVolume);
            if (_audioSource.volume < _desiredVolume) _audioSource.volume = Mathf.Min(_audioSource.volume + (_volumeFadeFactor * Time.deltaTime), 1f);
            else _audioSource.volume = Mathf.Max(_audioSource.volume - (_volumeFadeFactor * Time.deltaTime), 0f);
        }*/
        _desiredVolume = 1;
        if (GameConfiguration.Instance) _desiredVolume = GameConfiguration.Instance.GetSFXVolume();
        _audioSource.volume = _desiredVolume;
    }
}
