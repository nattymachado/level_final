using UnityEngine;
using System.Collections;

public class Switch1Behaviour : InteractableItemBehaviour
{
    [SerializeField] Animator gateAnimator;

    protected override void ExecuteAction(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (_isLocked || (character != null && !character.IsStoped()))
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
