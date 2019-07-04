using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState
{
    //Variables
    public GameEnums.FSMInteractionEnum interactionType;
    protected FSMController FSMControllerRef;

    //Constructor
    public FSMState(FSMController FSMControllerRef, GameEnums.FSMInteractionEnum interactionType)
    {
        this.FSMControllerRef = FSMControllerRef;
        this.interactionType = interactionType;
    }

    //Methods
    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();
}
