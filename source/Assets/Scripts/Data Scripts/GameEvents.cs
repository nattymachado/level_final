using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static class UIEvents
    {
        public static Action<GameEnums.PatientEnum> OpenPatientRecord;
        public static Action<bool> OpenMenu;
        public static Action OpenTutorialDialogBox;
        public static Action CloseTutorialDialogBox;
        public static Action TriggerItemsJoinAnimation;
    }

    public static class AudioEvents
    {
        public static Action<float> SetBGMVolume;
        public static Action<float> SetSFXVolume;
        public static Action<string> PlayBGM;
        public static Action<string, bool, bool> TriggerSFX; //(Trigger, Loop, Override)
        public static Action<string, bool, bool> TriggerRandomSFX; //(Trigger, Loop, Override)
        public static Action<string, Vector3> TriggerSFXOnPosition;
    }

    public static class FSMEvents
    {
        public static Action FinishedInteraction;
        public static Action<GameEnums.FSMInteractionEnum> StartInteraction;
    }

    public static class GameStateEvents
    {
        public static Action BGMSceneLoaded;
    }

    public static class LevelEvents
    {
        public static Action LevelIntroStarted;
        public static Action LevelStarted;
        public static Action Zoomed;
        public static Action Rotated;
        public static Action Clicked;
        public static Action Panned;
        public static Action Moved;
        public static Action UsedInteractable;
        public static Action PickedItem;
        public static Action SelectedItem;
        public static Action UsedItem;
        public static Action OpenedInventory;
        public static Action ClosedInventory;
    }
}
