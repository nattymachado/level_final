using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard2AudioSource : BaseSFXAudioController
{
    //Reference Variables
    [Header("Audio Clip Reference")]
    [SerializeField] protected AudioClip _audioClipKeycard;
    [SerializeField] protected AudioClip _audioClipPuzzleFail;
    [SerializeField] protected AudioClip _audioClipPuzzleSuccess;
    [SerializeField] protected AudioClip[] _audioClipButtonClick;

    //Start
    protected override void Start()
    {
        base.Start();
        GameEvents.RobotSceneAudioEvents.InsertedKeycardGreen += PlayKeycardClip;
        GameEvents.RobotSceneAudioEvents.FailedPuzzle2 += PlayPuzzleFailClip;
        GameEvents.RobotSceneAudioEvents.SuccessfulPuzzle2 += PlayPuzzleSucessClip;
        GameEvents.RobotSceneAudioEvents.Puzzle2ButtonClick += PlayRandomButtonClick;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        GameEvents.RobotSceneAudioEvents.InsertedKeycardGreen -= PlayKeycardClip;
        GameEvents.RobotSceneAudioEvents.FailedPuzzle2 -= PlayPuzzleFailClip;
        GameEvents.RobotSceneAudioEvents.SuccessfulPuzzle2 -= PlayPuzzleSucessClip;
        GameEvents.RobotSceneAudioEvents.Puzzle2ButtonClick -= PlayRandomButtonClick;

    }

    private void PlayKeycardClip()
    {
        PlayClip(_audioClipKeycard, false);
    }

    private void PlayPuzzleFailClip()
    {
        PlayClip(_audioClipPuzzleFail, false);
    }

    private void PlayPuzzleSucessClip()
    {
        PlayClip(_audioClipPuzzleSuccess, false);
    }

    private void PlayRandomButtonClick()
    {
        PlayClip(_audioClipButtonClick[Random.Range(0, _audioClipButtonClick.Length)], false);
    }
}
