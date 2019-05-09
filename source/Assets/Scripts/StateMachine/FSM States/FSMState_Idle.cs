using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Idle : FSMState
{
    //Constructor
    public FSMState_Idle(FSMController FSMControllerRef) : base(FSMControllerRef, GameEnums.FSMInteractionEnum.Idle) { }

    //Methods
    public override void OnStateEnter()
    {
        FSMControllerRef.LockedByInteraction = false;
        FSMControllerRef.characterBehavior.animator.SetBool("Moving", false);
    }

    public override void OnStateExit() { }

    public override void OnStateUpdate() { }
}
