using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkAudioSourceController : MonoBehaviour
{
    [SerializeField] private float _initialBarkDelay;
    [SerializeField] private float _minBarkDelay;
    [SerializeField] private float _maxBarkDelay;
    private float _currentTimer;

    // Start is called before the first frame update
    void Start()
    {
        _currentTimer = _initialBarkDelay;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTimer -= Time.deltaTime;
        if (_currentTimer <= 0f)
        {
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Bark", false, false);
            _currentTimer = Random.Range(_minBarkDelay, _maxBarkDelay);
        }
    }
}
