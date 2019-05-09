using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEnums
{
    public enum PatientEnum
    {
        None,
        Deliveryman,
        Security,
        Operator
    }

    public enum FSMInteractionEnum
    {
        None,
        Idle,
        PickupItem,
        ActivateItem,
        UseLadder,
    }
}
