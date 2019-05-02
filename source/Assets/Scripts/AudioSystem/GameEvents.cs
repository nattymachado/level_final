using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static class AudioEvents
    {
        public static Action<float> SetBGMVolume;
        public static Action<float> SetSFXVolume;
    }

    public static class RobotSceneAudioEvents
    {
        public static Action FailedPuzzle1;
        public static Action FailedPuzzle2;
        public static Action SuccessfulPuzzle1;
        public static Action SuccessfulPuzzle2;
        public static Action InsertedKeycardBlack1;
        public static Action InsertedKeycardBlack2;
        public static Action InsertedKeycardGreen;
        public static Action InsertedKeycardRed;
        public static Action Puzzle1ButtonClick;
        public static Action Puzzle2ButtonClick;
    }
}
