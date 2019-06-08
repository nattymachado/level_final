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
        FSMControllerRef.characterBehavior.animator.SetBool("PickupItem", true);
    }

    public override void OnStateExit()
    {
        FSMControllerRef.characterBehavior.animator.SetBool("PickupItem", false);
        FSMControllerRef.LockedByInteraction = false;
        FSMControllerRef.SetNextState(GameEnums.FSMInteractionEnum.Idle);
    }

    public override void OnStateUpdate() { }
}
