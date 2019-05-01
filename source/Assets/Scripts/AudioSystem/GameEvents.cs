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
}
