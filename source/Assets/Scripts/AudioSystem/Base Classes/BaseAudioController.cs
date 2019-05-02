using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAudioController : MonoBehaviour
{
    //Control Variables
    [Header("General Configurations")]
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected float _baseVolume;

    //Internal Variables
    protected float _desiredVolume;

    // Use this for initialization
    protected virtual void Start()
    {
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 0f;
        _desiredVolume = _baseVolume;
    }

    //Adjust Volume
    protected virtual void SetDesiredVolume(float newVolume)
    {
        if(newVolume > _baseVolume) _desiredVolume = Mathf.Min(_baseVolume + (newVolume - _baseVolume), 1f);
        else _desiredVolume = Mathf.Max(_baseVolume + (_baseVolume - newVolume), 0f);
    }
}

