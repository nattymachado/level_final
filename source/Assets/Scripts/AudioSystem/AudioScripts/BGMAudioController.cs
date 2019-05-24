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
        _audioType = GameEnums.AudioTypeEnum.BGM;
        GameEvents.AudioEvents.SetBGMVolume += SetDesiredVolume;
    }

    //Adjust Volume
    protected override void SetDesiredVolume(float newVolume)
    {
        /*if(newVolume > _baseVolume) _desiredVolume = Mathf.Min(_baseVolume + (newVolume - _baseVolume), 1f);
        else _desiredVolume = Mathf.Max(_baseVolume + (_baseVolume - newVolume), 0f);*/
        _desiredVolume = newVolume;
        GameConfiguration.Instance.SetBGMVolume(_desiredVolume);
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetBGMVolume -= SetDesiredVolume;
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
        if (GameConfiguration.Instance) _desiredVolume = GameConfiguration.Instance.GetBGMVolume();
        _audioSource.volume = _desiredVolume;

    }
}
