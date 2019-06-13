using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialItemJoinAnimationController : MonoBehaviour
{
    [SerializeField] private Image[] _images;
    [SerializeField] private GameObject _items;
    [SerializeField] private GameObject _specialSlotsCanvas;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        if (_images != null && _images.Length > 0)
        {
            SpecialItemCanvasBehaviour[] animationItems = _items.GetComponentsInChildren<SpecialItemCanvasBehaviour>();
            for (int i = 0; i < animationItems.Length; i++)
            {
                if (_images[i]!= null)
                    animationItems[i].GetComponentInChildren<Image>().sprite = _images[i].sprite;


            }
            GameEvents.UIEvents.TriggerItemsJoinAnimation += Animate;
        }
        
        
    }

    private void OnDestroy()
    {
        if (_images != null)
            GameEvents.UIEvents.TriggerItemsJoinAnimation -= Animate;
    }

    private void Animate()
    {
        _animator.SetTrigger("Animate");
        _specialSlotsCanvas.GetComponent<Animator>().SetTrigger("Hide");
    }
}
