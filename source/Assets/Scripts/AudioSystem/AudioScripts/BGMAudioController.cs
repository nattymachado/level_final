using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMAudioController : BaseAudioController
{
    //Control Variables
    [Header("Specific Configurations")]
    [SerializeField] private float _volumeFadeFactor;
    private BGMAudioInfo _currentClip;
    private BGMAudioInfo _newClip;
    private float _desiredVolume;

    //Reference Variables
    [Header("Audio Clips")]
    [SerializeField] private BGMAudioInfo[] _BGMClips;

    //Awake
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        _audioSource.loop = true;
        _audioSource.spatialBlend = 0f;
        _audioSource.Stop();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        SetDesiredVolume(GameConfiguration.Instance.GetBGMVolume());
        GameEvents.AudioEvents.SetBGMVolume += SetDesiredVolume;
        GameEvents.AudioEvents.PlayBGM += PlayBGM;
        GameEvents.GameStateEvents.BGMSceneLoaded.SafeInvoke();
    }

    //OnDestroy Memory Leak Safeguard
    protected virtual void OnDestroy()
    {
        GameEvents.AudioEvents.SetBGMVolume -= SetDesiredVolume;
        GameEvents.AudioEvents.PlayBGM -= PlayBGM;
    }

    //Adjust Volume
    protected override void SetDesiredVolume(float newVolume)
    {
        if(_currentClip == null) _desiredVolume = newVolume * _baseVolumeFactor;
        else _desiredVolume = newVolume * _baseVolumeFactor * _currentClip.volumeFactor;
        _audioSource.volume = _desiredVolume;
    }

    //Play BGM
    private void PlayBGM(string trigger)
    {
        foreach(BGMAudioInfo tmp in _BGMClips)
        {
            if(tmp.trigger.Equals(trigger))
            {
                if (_audioSource.clip == tmp.audioClip) return; //Already playing
                else
                {
                    _newClip = tmp;
                    return;
                }
            }
        }
    }

    //Update
    private void Update()
    {
        if(_newClip != null)
        {
            if(!_audioSource.isPlaying || _audioSource.volume == 0f)
            {
                _audioSource.clip = _newClip.audioClip;
                _currentClip = _newClip;
                _desiredVolume = GameConfiguration.Instance.GetBGMVolume() * _baseVolumeFactor * _currentClip.volumeFactor;
                _newClip = null;

                if(!_audioSource.isPlaying)
                {
                    _audioSource.volume = _desiredVolume;
                    _audioSource.Play();
                }
            }
            else if(_audioSource.volume > 0f) _audioSource.volume = Mathf.Max(_audioSource.volume - (_volumeFadeFactor * Time.deltaTime), 0f);
        }
        else if(_audioSource.volume != _desiredVolume)
        {
            if (_audioSource.volume > _desiredVolume) _audioSource.volume = Mathf.Max(_audioSource.volume - (_volumeFadeFactor * Time.deltaTime), _desiredVolume);
            else _audioSource.volume = Mathf.Min(_audioSource.volume + (_volumeFadeFactor * Time.deltaTime), _desiredVolume);
        }
    }
}
