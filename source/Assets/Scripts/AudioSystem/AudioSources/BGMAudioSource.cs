using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMAudioSource : BaseAudioController
{
    //Control Variables
    [Header("Specific Configurations")]
    [SerializeField] private float _volumeFadeFactor;

    //Reference Variables
    [Header("Audio Clip")]
    [SerializeField] private AudioClip _audioClip;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _audioSource.clip = _audioClip;
        _audioSource.loop = true;
        _audioSource.spatialBlend = 0f;
        _audioSource.Play();
        GameEvents.AudioEvents.SetBGMVolume += SetDesiredVolume;
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetBGMVolume -= SetDesiredVolume;
    }

    //Update
    protected virtual void Update()
    {
        if (_audioSource.volume != _desiredVolume)
        {
            if (_audioSource.volume < _desiredVolume) _audioSource.volume = Mathf.Min(_audioSource.volume + (_volumeFadeFactor * Time.deltaTime), 1f);
            else _audioSource.volume = Mathf.Max(_audioSource.volume - (_volumeFadeFactor * Time.deltaTime), 0f);
        }
    }
}
