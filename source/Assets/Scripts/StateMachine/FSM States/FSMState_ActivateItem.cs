using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMState_ActivateItem : FSMState
{
    //Constructor
    public FSMState_ActivateItem(FSMController FSMControllerRef) : base(FSMControllerRef, GameEnums.FSMInteractionEnum.ActivateItem) { }

    //Methods
    public override void OnStateEnter()
    {
        FSMControllerRef.LockedByInteraction = true;
        FSMControllerRef.characterBehavior.animator.SetTrigger("ActivateItem");
    }

    public override void OnStateExit()
    {
        FSMControllerRef.LockedByInteraction = false;
        FSMControllerRef.SetNextState(GameEnums.FSMInteractionEnum.Idle);
    }

    public override void OnStateUpdate() { }
}
