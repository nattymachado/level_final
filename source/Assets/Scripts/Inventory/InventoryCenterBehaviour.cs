using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;


public class InventoryCenterBehaviour : MonoBehaviour
{
    [SerializeField] private InventoryObjectBehaviour _item;
    [SerializeField] private InventoryObjectBehaviour _specialItem;
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private List<InventoryItemBehaviour> _items;
    [SerializeField] private List<string> _clientItems;
    [SerializeField] private Image _centerImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private CanvasGroup canvasGroup;
    private Image inventaryBackground;
    private bool _isOpen = false;

    private void Awake()
    {
        inventaryBackground = GetComponent<Image>();
    }

    public void AddNewItem(InventoryObjectBehaviour item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].IsEmpty())
            {
                _items[i].AddItem(item);

                // trigger event
                GameEvents.LevelEvents.PickedItem.SafeInvoke();

                break;
            }
        }
        CheckClientItems();
    }

    IEnumerator WaitToGetSpecialItem(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameEvents.UIEvents.TriggerItemsJoinAnimation.SafeInvoke();
        StartCoroutine(WaitToStartSpecialItemAnimation(1.5f));
    }

    IEnumerator WaitToStartSpecialItemAnimation(float seconds)
    {
       yield return new WaitForSeconds(seconds);
       for (int i = 0; i < _items.Count; i++)
        {
            _items[i].RemoveItem();
        }
        AddNewItem(_specialItem);
    }

    public void CheckClientItems()
    {

        bool haveAllItems = true;
        for (int i = 0; i < _clientItems.Count; i++)
        {
            IEnumerable<InventoryItemBehaviour> hasItem = _items.Where(x => x.Item != null && x.Item.Name == _clientItems[i]);
            if (!hasItem.Any())
            {
                haveAllItems = false;
                break;
            }
        }

        if (haveAllItems)
        {
            StartCoroutine(WaitToGetSpecialItem(0.15f));
        }

    }

    public void RemoveNewItem(string name)
    {
        InventoryItemBehaviour item = _items.Find(x => x.Item != null && x.Item.Name == name);
        if (item)
        {
            item.RemoveItem();
        }

    }

    public void SelectItem(InventoryObjectBehaviour item)
    {
        if (item != null)
        {
            if (_item != null)
            {
                AddNewItem(_item);
            }
            _item = item;
            _centerImage.enabled = true;
            _centerImage.sprite = _item.objectImage.sprite;
            RemoveNewItem(_item.Name);
            CloseOrOpen();

            // trigger event
            GameEvents.LevelEvents.SelectedItem.SafeInvoke();
        }

    }

    public bool CheckItem(string itemName)
    {
        Debug.Log(_item);
        return _item != null && _item.Name == itemName;
    }

    private void ClearSelection()
    {
        _item = null;
        _centerImage.enabled = false;
        _centerImage.sprite = null;
    }

    public void UseSelectedItem()
    {
        _item = null;
        ShowBag();
    }

    public void ShowBag()
    {
        _centerImage.enabled = true;
        _centerImage.sprite = _emptySprite;
    }

    private void SetAnimation(bool status)
    {
        // executa a animação
        _animator.SetBool("opened", status);
    }

    public void OnClick()
    {
        if (_item != null)
        {
            AddNewItem(_item);
        }
        ClearSelection();
        CloseOrOpen();

        // trigger event
        if (_isOpen)
        {
            GameEvents.LevelEvents.OpenedInventory.SafeInvoke();
        }
        else
        {
            GameEvents.LevelEvents.ClosedInventory.SafeInvoke();
        }
    }

    public void CloseOrOpen()
    {
        _isOpen = !_isOpen;
        SetAnimation(_isOpen);
        if (!_isOpen && _item == null) ShowBag();
    }

    public void EnableDisable(bool enabled)
    {
        canvasGroup.interactable = enabled;
        canvasGroup.blocksRaycasts = enabled;
        inventaryBackground.color = enabled ? Color.white : Color.gray;
        _centerImage.color = enabled ? Color.white : new Color(1, 1, 1, 0.5f);
    }
}
