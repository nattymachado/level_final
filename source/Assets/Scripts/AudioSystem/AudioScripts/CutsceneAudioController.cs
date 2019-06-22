using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneAudioController : BaseAudioController
{
    //Required Data
    [SerializeField] private float _volumeFadeFactor;
    [SerializeField] private CutSceneBehaviour _cutSceneBehaviour;

    //Audio Clip References
    [Header("Audio Clip References")]
    [SerializeField] private AudioClipTriggerInfo[] _audioClips;

    //Control Variables
    private bool fadeout = false;

    //Start
    protected override void Awake()
    {
        base.Awake();
        _audioSource.loop = false;
        _audioSource.spatialBlend = 0f;
        GameEvents.AudioEvents.SetSFXVolume += SetDesiredVolume;
        GameEvents.AudioEvents.TriggerSFX += TriggerAudioClip;
        _cutSceneBehaviour.beforeFadeout = FadeoutSFX;
    }

    private void Start()
    {
        SetDesiredVolume(GameConfiguration.Instance.GetSFXVolume());
        foreach(AudioClipTriggerInfo triggerInfo in _audioClips)
        {
            PlayClip(triggerInfo.audioClip, false);
        }
    }

    //Adjust Volume
    protected override void SetDesiredVolume(float newVolume)
    {
        _audioSource.volume = newVolume * _baseVolumeFactor;
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetSFXVolume -= SetDesiredVolume;
        GameEvents.AudioEvents.TriggerSFX -= TriggerAudioClip;
    }

    public void FadeoutSFX()
    {
        fadeout = true;
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

    private void Update()
    {
        if(fadeout)
        {
            SetDesiredVolume(Mathf.Max(0f, _audioSource.volume - (Time.deltaTime * _volumeFadeFactor)));
        }
    }
}
