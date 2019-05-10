using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController
{
    //Public Variables
    public CharacterBehaviour characterBehavior;

    //Control Variables
    private bool lockedByInteraction;
    private FSMState _currentState;
    private FSMState _nextState;
    private bool _requestChangeState;
    private Dictionary<GameEnums.FSMInteractionEnum, FSMState> _dictionaryEnumToState;

    //Properties
    public bool LockedByInteraction
    {
        get
        {
            return lockedByInteraction;
        }
        set
        {
            characterBehavior.SetNavMeshStopped(value);
            lockedByInteraction = value;
        }
    }

    //Constructor
    public FSMController(CharacterBehaviour characterBehavior)
    {
        this.characterBehavior = characterBehavior;
        _requestChangeState = false;
        lockedByInteraction = false;
        SetupStates();
        SetupEvents();
    }

    //Setup Events Subscriptions
    private void SetupEvents()
    {
        GameEvents.FSMEvents.StartInteraction += SetNextState;
        GameEvents.FSMEvents.FinishedInteraction += FinishInteraction;
    }

    //OnDestroy Memory Leak Safeguard (Not MonoBehavior! Call from Parent!)
    public void OnDestroy()
    {
        GameEvents.FSMEvents.StartInteraction -= SetNextState;
        GameEvents.FSMEvents.FinishedInteraction -= FinishInteraction;
    }

    //Setup States
    private void SetupStates()
    {
        //Setup Dictionary
        _dictionaryEnumToState = new Dictionary<GameEnums.FSMInteractionEnum, FSMState>();

        //Create States
        _dictionaryEnumToState.Add(GameEnums.FSMInteractionEnum.Idle, new FSMState_Idle(this));
        _dictionaryEnumToState.Add(GameEnums.FSMInteractionEnum.Moving, new FSMState_Moving(this));
        _dictionaryEnumToState.Add(GameEnums.FSMInteractionEnum.ActivateItem, new FSMState_ActivateItem(this));
        _dictionaryEnumToState.Add(GameEnums.FSMInteractionEnum.PickupItem, new FSMState_PickupItem(this));

        //Finally...
        _currentState = _dictionaryEnumToState[GameEnums.FSMInteractionEnum.Idle];
    }

    //Set Next State
    public void SetNextState(GameEnums.FSMInteractionEnum requestedAction)
    {
        FSMState state;
        if(_dictionaryEnumToState.TryGetValue(requestedAction, out state))
        {
            if (state != _currentState)
            {
                _nextState = state;
                _requestChangeState = true;
            }
        }
    }

    //Finished Interaction
    private void FinishInteraction()
    {
        _requestChangeState = true;
    }

    //Update
    public void UpdateFSM()
    {
        if(_requestChangeState)
        {
            _currentState.OnStateExit();
            if(_nextState == null) _nextState = _dictionaryEnumToState[GameEnums.FSMInteractionEnum.Idle];
            _currentState = _nextState;
            _currentState.OnStateEnter();
            _nextState = null;
            _requestChangeState = false;
        }
        else
        {
            _currentState.OnStateUpdate();
        }
    }
}
