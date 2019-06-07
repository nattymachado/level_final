using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_EnterOnPortal : FSMState
{
    //Constructor
    public FSMState_EnterOnPortal(FSMController FSMControllerRef) : base(FSMControllerRef, GameEnums.FSMInteractionEnum.EnterOnPortal) { }

    //Methods
    public override void OnStateEnter()
    {
        FSMControllerRef.characterBehavior.animator.SetTrigger("EnterOnPortal");
    }

    public override void OnStateExit()
    {
        FSMControllerRef.SetNextState(GameEnums.FSMInteractionEnum.Idle);
    }

    public override void OnStateUpdate() { }
}