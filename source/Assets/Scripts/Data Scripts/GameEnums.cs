﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEnums
{
    public enum PatientEnum
    {
        None,
        Deliveryman,
        Security,
        Operator,
        Singer
    }

    public enum LevelName {
        NULL,
        Robot,
        Night,
        Dog,
        Light
    }

    public enum FSMInteractionEnum
    {
        None,
        Idle,
        Moving,
        PickupItem,
        ActivateItem,
        UseLadder,
        Victory,
        EnterOnPortal,
        ExitOfPortal
    }

    public enum AudioTypeEnum
    {
        BGM,
        SFX,
        UISFX
    }

    public enum ItemTypeEnum
    {
        None,
        Generic,
        Collectible
    }
}
