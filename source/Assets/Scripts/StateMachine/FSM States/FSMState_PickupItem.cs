using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_PickupItem : FSMState
{
    //Constructor
    public FSMState_PickupItem(FSMController FSMControllerRef) : base(FSMControllerRef, GameEnums.FSMInteractionEnum.PickupItem) { }

    public override void OnStateEnter()
    {
        FSMControllerRef.LockedByInteraction = true;
    }

    public override void OnStateExit()
    {
        FSMControllerRef.SetNextState(GameEnums.FSMInteractionEnum.Idle);
    }

    public override void OnStateUpdate() { }
}
