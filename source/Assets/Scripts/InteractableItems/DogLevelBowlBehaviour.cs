using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLevelBowlBehaviour : InteractableItemBehaviour
{
    [SerializeField] CharacterBehaviour character;
    [SerializeField] string activatorItemName;
    [SerializeField] Animator fallingBowlAnimator;
    [SerializeField] Animator selfAnimator;
    [SerializeField] DogLevelDoggy dog;
    [SerializeField] GameObject dogFood;
    private bool activated = false;
    private bool revealed = false;

    protected override void ExecuteAction(Collider other)
    {
       if (character && revealed && !activated && character.CheckInventaryObjectOnSelectedPosition(activatorItemName))
        {
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Pour", false, false);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            dogFood.SetActive(true);
            ActivateBowl();
        } 
    }

    private void ActivateBowl(){
        dog.GoToBowl();
    }

    public void Show(){
        StartCoroutine(Reveal());
    }

    private IEnumerator Reveal(){
        fallingBowlAnimator.SetTrigger("fall");
        yield return new WaitForSeconds(1f);
        selfAnimator.SetTrigger("show");
        yield return new WaitForSeconds(0.5f);
        revealed = true;
        Debug.Log("bowl shown");
    }

}
