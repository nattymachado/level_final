using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAudioController : MonoBehaviour
{
    //Control Variables
    [Header("General Configurations")]
    [SerializeField] protected float _baseVolumeFactor = 1f;
    [SerializeField] protected AudioSource _audioSource;

    // Use this for initialization
    protected virtual void Awake()
    {
        _audioSource.playOnAwake = false;
        _audioSource.Stop();
        _audioSource.spatialBlend = 0f;
        _audioSource.volume = _baseVolumeFactor;
    }

    //Adjust Volume
    protected virtual void SetDesiredVolume(float newVolume) { }
}

