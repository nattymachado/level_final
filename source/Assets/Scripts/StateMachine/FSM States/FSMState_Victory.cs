using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Victory : FSMState
{
    //Constructor
    public FSMState_Victory(FSMController FSMControllerRef) : base(FSMControllerRef, GameEnums.FSMInteractionEnum.Victory) { }

    //Methods
    public override void OnStateEnter()
    {
        FSMControllerRef.characterBehavior.animator.SetTrigger("Victory");
        FSMControllerRef.LockedByInteraction = true;
    }

    public override void OnStateExit()
    {
        FSMControllerRef.SetNextState(GameEnums.FSMInteractionEnum.Idle);
    }

    public override void OnStateUpdate() { }
}