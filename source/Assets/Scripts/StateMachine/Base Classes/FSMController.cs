using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController : MonoBehaviour
{
    //Control Variables
    [HideInInspector] public bool lockedByInteraction;
    private FSMState _currentState;
    private FSMState _nextState;
    private bool _requestChangeState;
    private Dictionary<GameEnums.FSMInteractionEnum, FSMState> _dictionaryEnumToState;

    //Constructor
    public FSMController()
    {
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

    //OnDestroy Memory Leak Safeguard
    private void OnDestroy()
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
            _nextState = state;
        }
    }

    //Finished Interaction
    private void FinishInteraction(GameEnums.FSMInteractionEnum finishedAction)
    {
        if (_currentState.interactionType == finishedAction) _requestChangeState = true;
    }

    //Update
    private void Update()
    {
        if(_requestChangeState)
        {
            _currentState.OnStateExit();
            if(_nextState == null) _nextState = _dictionaryEnumToState[GameEnums.FSMInteractionEnum.Idle];
            _currentState = _nextState;
            _currentState.OnStateEnter();
            _nextState = null;
        }
        else
        {
            _currentState.OnStateUpdate();
        }
    }
}
