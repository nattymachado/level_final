using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickupAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Image _imageRef;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _imageRef = this.GetComponent<Image>();
        GameEvents.UIEvents.TriggerItemPickupAnimation += Animate;
    }

    private void OnDestroy()
    {
        GameEvents.UIEvents.TriggerItemPickupAnimation -= Animate;
    }

    private void Animate(Sprite sprite)
    {
        _imageRef.sprite = sprite;
        _animator.SetTrigger("Animate");
    }
}
