using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialItemJoinAnimationController : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        GameEvents.UIEvents.TriggerItemsJoinAnimation += Animate;
    }

    private void OnDestroy()
    {
        GameEvents.UIEvents.TriggerItemsJoinAnimation -= Animate;
    }

    private void Animate()
    {
        _animator.SetTrigger("Animate");
    }
}
