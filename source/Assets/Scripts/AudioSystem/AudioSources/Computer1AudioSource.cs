using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer1AudioSource : BaseSFXAudioController
{
    //Reference Variables
    [Header("Audio Clip Reference")]
    [SerializeField] protected AudioClip _audioClipPuzzleFail;
    [SerializeField] protected AudioClip _audioClipPuzzleSuccess;
    [SerializeField] protected AudioClip[] _audioClipButtonClick;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        GameEvents.RobotSceneAudioEvents.FailedPuzzle1 += PlayPuzzleFail;
        GameEvents.RobotSceneAudioEvents.SuccessfulPuzzle1 += PlayPuzzleSuccess;
        GameEvents.RobotSceneAudioEvents.Puzzle1ButtonClick += PlayRandomButtonClick;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvents.RobotSceneAudioEvents.FailedPuzzle1 -= PlayPuzzleFail;
        GameEvents.RobotSceneAudioEvents.SuccessfulPuzzle1 -= PlayPuzzleSuccess;
        GameEvents.RobotSceneAudioEvents.Puzzle1ButtonClick -= PlayRandomButtonClick;
    }

    private void PlayPuzzleFail()
    {
        PlayClip(_audioClipPuzzleFail, false);
    }

    private void PlayPuzzleSuccess()
    {
        PlayClip(_audioClipPuzzleSuccess, false);
    }

    private void PlayRandomButtonClick()
    {
        if (_audioSource != null && !_audioSource.isPlaying) PlayClip(_audioClipButtonClick[Random.Range(0, _audioClipButtonClick.Length)], false);
    }
}
