using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
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
                GetItem(character);
            }
        }
    }

    private void GetItem(CharacterBehaviour character)
    {
        character.SetRotation(transform);
        
        
        StartCoroutine(WaitToIncludeOnInventory(0f, character));
    }

    IEnumerator WaitToIncludeOnInventory(float seconds, CharacterBehaviour character)
    {
        yield return new WaitForSeconds(seconds);
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
        IncludeItemOnInventory(character);
    }

    private void IncludeItemOnInventory(CharacterBehaviour character)
    {
        GameEvents.AudioEvents.TriggerSFXOnPosition.SafeInvoke("ItemPickup", this.transform.position);
        if (_animator != null)
        {
            _animator.SetBool("IsGoingToInventary", true);
        }
        inventaryCenter.AddNewItem(this);
    }

    public void DisableItem()
    {
        gameObject.SetActive(false);
    }

    public void AnimateItemPickup()
    {
        GameEvents.UIEvents.TriggerItemPickupAnimation.SafeInvoke(objectImage.sprite);
    }
}
