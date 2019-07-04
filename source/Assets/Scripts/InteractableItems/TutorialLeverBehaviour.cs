using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLeverBehaviour : InteractableItemBehaviour
{
  [SerializeField] ParticleSystem particles;

  private void Start(){
    SetActive(false);
  }

  protected override void ExecuteAction(CharacterBehaviour character)
  {
        // trigger event
        SetActive(false);
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
        StartCoroutine(WaitToOpenDoor(0.8f));
  }

  IEnumerator WaitToOpenDoor(float seconds)
  {
        yield return new WaitForSeconds(seconds);
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Lever", false, false);
        transform.Rotate(0, 0, 180);
        GameEvents.LevelEvents.UsedInteractable.SafeInvoke();
    }

  public void TurnOn()
  {
    Activate();
    particles.Play();
  }

  public void TurnOff()
  {
    particles.Stop();
  }
}
