using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_ExitOfPortal : FSMState
{
    //Constructor
    public FSMState_ExitOfPortal(FSMController FSMControllerRef) : base(FSMControllerRef, GameEnums.FSMInteractionEnum.ExitOfPortal) { }

    //Methods
    public override void OnStateEnter()
    {
        FSMControllerRef.characterBehavior.animator.SetTrigger("ExitOfPortal");
        FSMControllerRef.LockedByInteraction = true;
    }

    public override void OnStateExit()
    {
        FSMControllerRef.SetNextState(GameEnums.FSMInteractionEnum.Idle);
    }

    public override void OnStateUpdate() { }
}