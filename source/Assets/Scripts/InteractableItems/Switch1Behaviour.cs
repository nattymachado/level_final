using UnityEngine;
using System.Collections;

public class Switch1Behaviour : InteractableItemBehaviour
{
    [SerializeField] Animator gateAnimator;
    private bool _isLocked=false;

    protected override void ExecuteAction(Collider other)
    {
        if (_isLocked)
        {
            return;
        }
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.PickupItem);
        transform.Rotate(0, -180, 0);
        gateAnimator.SetBool("isOpen", true);
        SetActive(false);
        _isLocked = true;
    }


}
