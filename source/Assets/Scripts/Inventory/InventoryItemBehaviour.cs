using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class InventoryItemBehaviour : MonoBehaviour
{
    [SerializeField] public InventoryObjectBehaviour Item;
    [SerializeField] private InventoryCenterBehaviour _inventaryCenter;
    [SerializeField] private Image _itemImage;

    public void AddItem(InventoryObjectBehaviour item)
    {
        this.Item = item;
        _itemImage.enabled = true;
        _itemImage.sprite = Item.objectImage.sprite;
    }

    public void RemoveItem()
    {
        Item = null;
        _itemImage.sprite = null;
        _itemImage.enabled = false;
    }

    public bool IsEmpty()
    {
        return Item == null;
    }

    public void OnClick() => _inventaryCenter.SelectItem(Item);

}

