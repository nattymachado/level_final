using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class InventoryObjectBehaviour : MonoBehaviour
{

    [SerializeField] public string Name;
    [SerializeField] public Image objectImage;
    [SerializeField] public InventoryCenterBehaviour inventaryCenter;
    private Animator _animator;
    private bool _isEnabled = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isEnabled)
        {
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            if (character != null)
            {
                _isEnabled = false;
                GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
                StartCoroutine(WaitToIncludeOnInventory(1f));
            }
        }
    }

    IEnumerator WaitToIncludeOnInventory(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        IncludeItemOnInventory();
    }

    private void IncludeItemOnInventory()
    {
        GameEvents.AudioEvents.TriggerSFXOnPosition.SafeInvoke("ItemPickup", this.transform.position);
        if (_animator != null)
        {
            _animator.SetBool("IsGoingToInventary", true);
        }
        inventaryCenter.AddNewItem(this);
        if (_animator == null)
        {
            gameObject.SetActive(false);
        }
    }

    public void AnimateItemPickup()
    {
        GameEvents.UIEvents.TriggerItemPickupAnimation.SafeInvoke(objectImage.sprite);
    }
}
