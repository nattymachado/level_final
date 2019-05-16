using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_Moving : FSMState
{
    //Constructor
    public FSMState_Moving(FSMController FSMControllerRef) : base(FSMControllerRef, GameEnums.FSMInteractionEnum.Moving) { }

    //Methods
    public override void OnStateEnter()
    {
        FSMControllerRef.LockedByInteraction = false;
        FSMControllerRef.characterBehavior.animator.SetBool("Moving", true);
    }

    public override void OnStateExit()
    {
        FSMControllerRef.characterBehavior.animator.SetBool("Moving", false);
    }

    public override void OnStateUpdate() { }
}
