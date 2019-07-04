using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_UseLadder : FSMState
{
    //Constructor
    public FSMState_UseLadder(FSMController FSMControllerRef) : base(FSMControllerRef, GameEnums.FSMInteractionEnum.UseLadder) { }

    //Methods
    public override void OnStateEnter()
    {
        FSMControllerRef.characterBehavior.animator.SetBool("UsingLadder", true);
        FSMControllerRef.LockedByInteraction = true;
    }

    public override void OnStateExit()
    {
        FSMControllerRef.characterBehavior.animator.SetBool("UsingLadder", false);
        FSMControllerRef.SetNextState(GameEnums.FSMInteractionEnum.Idle);
        FSMControllerRef.LockedByInteraction = false;
    }

    public override void OnStateUpdate() { }
}