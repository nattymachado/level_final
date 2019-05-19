using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAudioSourceController : MonoBehaviour
{
    //Control Variables
    [Header("Control Variables")]
    [SerializeField] private float _timeToNotificationRepeat;
    [SerializeField] private float _initialDelay;
    [SerializeField] private float _delayNotifications;

    //Internal Variables
    private float _currentTimer;
    private int _currentAudio;

    //Start
    private void Start()
    {
        _currentTimer = _initialDelay;
        _currentAudio = 0;
    }

    //Update
    private void Update()
    {
        _currentTimer -= Time.deltaTime;
        if (_currentTimer <= 0f)
        {
            if (_currentAudio == 0)
            {
                _currentAudio = 1;
                GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Alarm", false, false);
                _currentTimer = _delayNotifications;
            }
            else if (_currentAudio == 1)
            {
                _currentAudio = 2;
                GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Transmission", false, false);
                _currentTimer = _delayNotifications;
            }
            else if (_currentAudio == 2)
            {
                _currentAudio = 0;
                GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Binary", false, false);
                _currentTimer = _timeToNotificationRepeat;
            }
        }
    }
}
