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
        FSMControllerRef.lockedByInteraction = true;
    }

    public override void OnStateExit()
    {
        FSMControllerRef.SetNextState(GameEnums.FSMInteractionEnum.Idle);
    }

    public override void OnStateUpdate() { }
}
