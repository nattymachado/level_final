using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialItemJoinAnimationController : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private GameObject _items;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        SpecialItemCanvasBehaviour[] animationItems = _items.GetComponentsInChildren<SpecialItemCanvasBehaviour>();
        Debug.Log("Items:" + animationItems.Length);
        Debug.Log("Images:" + _images.Length);
        for (int i=0; i < animationItems.Length; i++)
        {
            animationItems[i].GetComponentInChildren<Image>().sprite = _images[i].sprite;
        }
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
