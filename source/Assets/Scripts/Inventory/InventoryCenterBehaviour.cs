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
    private bool _isOpen = false;

    public void AddNewItem(InventoryObjectBehaviour item)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].IsEmpty())
            {
                _items[i].AddItem(item);
                break;
            }
        }
        CheckClientItems();
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
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].RemoveItem();
            }
            AddNewItem(_specialItem);

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
        }

    }

    public bool CheckItem(string itemName)
    {
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
    }

    public void CloseOrOpen()
    {
        _isOpen = !_isOpen;
        SetAnimation(_isOpen);
        if (!_isOpen && _item == null) ShowBag();
    }
}
