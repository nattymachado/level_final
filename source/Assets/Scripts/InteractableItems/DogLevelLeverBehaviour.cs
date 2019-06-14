﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLevelLeverBehaviour : InteractableItemBehaviour
{
    [SerializeField] private string sfxtrigger;
    [SerializeField] private DogLevelDoor door;

    protected override void ExecuteAction(Collider other)
    {
        // trigger event
        SetActive(false);
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
        StartCoroutine(WaitToOpenDoor(1f));
    }

    IEnumerator WaitToOpenDoor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.Rotate(0, 0, 180);
        GameEvents.LevelEvents.UsedInteractable.SafeInvoke();
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke(sfxtrigger, false, false);
        door.Toggle();
    }
}
