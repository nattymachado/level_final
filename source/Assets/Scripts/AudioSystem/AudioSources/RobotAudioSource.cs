using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAudioSource : BaseSFXAudioController
{
    //Reference Variables
    [Header("Audio Clips References")]
    [SerializeField] private AudioClip _robotAlarm;
    [SerializeField] private AudioClip _robotTransmissionAlert;
    [SerializeField] private AudioClip _robotTransmissionData;

    //Control Variables
    [Header("Control Variables")]
    [SerializeField] private float _timeToNotification;
    [SerializeField] private float _initialDelay;

    //Internal Variables
    private float _currentTimer;
    private int _currentAudio;

    //Start
    protected override void Start()
    {
        base.Start();
        _currentTimer = _initialDelay;
        _currentAudio = 0;
    }

    //Update
    private void Update()
    {
        _currentTimer -= Time.deltaTime;
        if (_currentTimer <= 0f)
        {
            if (_currentAudio == 0 && !_audioSource.isPlaying)
            {
                _currentAudio = 1;
                PlayClip(_robotAlarm, false);
                _currentTimer = 1f;
            }
            else if (_currentAudio == 1 && !_audioSource.isPlaying)
            {
                _currentAudio = 2;
                PlayClip(_robotTransmissionAlert, false);
                _currentTimer = 1f;
            }
            else if (_currentAudio == 2 && !_audioSource.isPlaying)
            {
                _currentAudio = 0;
                PlayClip(_robotTransmissionData, false);
                _currentTimer = _timeToNotification;
            }
        }
    }
}
