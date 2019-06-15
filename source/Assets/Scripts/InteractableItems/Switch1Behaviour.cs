using UnityEngine;
using System.Collections;

public class Switch1Behaviour : InteractableItemBehaviour
{
    [SerializeField] Animator gateAnimator;

    protected override void ExecuteAction(CharacterBehaviour character)
    {
        if (_isLocked)
        {
            return;
        }

        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
        _isLocked = true;
        StartCoroutine(WaitToOpenGate(0.7f));
    }

    IEnumerator WaitToOpenGate(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        transform.Rotate(0, -180, 0);
        gateAnimator.SetBool("isOpen", true);
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Lever1", false, false);
        SetActive(false);
    }
}
