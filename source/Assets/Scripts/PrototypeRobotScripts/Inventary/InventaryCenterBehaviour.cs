using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace prototypeRobot
{

    public class InventaryCenterBehaviour : MonoBehaviour
    {
        [SerializeField] private InventaryObjectBehaviour _item;
        [SerializeField] private InventaryObjectBehaviour _specialItem;
        [SerializeField] private Sprite _emptySprite;
        [SerializeField] private List<InventaryItemBehaviour> _items;
        [SerializeField] private List<string> _clientItems;
        private bool _isShowingItems = false;

        public void AddNewItem(InventaryObjectBehaviour item)
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
                IEnumerable<InventaryItemBehaviour> hasItem = _items.Where(x => x.Item != null &&  x.Item.Name == _clientItems[i]);
                Debug.Log(hasItem.Any());
                Debug.Log(hasItem);
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
            InventaryItemBehaviour item = _items.Find(x => x.Item != null && x.Item.Name == name);
            if (item)
            {
                item.RemoveItem();
            }

        }

        public void SelectItem(InventaryObjectBehaviour item)
        {
            if (item != null)
            {
                if (_item != null)
                {
                    AddNewItem(_item);
                }
                _item = item;
                GetComponent<Image>().sprite = _item.objectImageCentrer.sprite;
                RemoveNewItem(_item.Name);
            }
            
        }

        public bool CheckItem(string itemName)
        {
            return _item != null &&_item.Name == itemName;
        }

        public void ClearSelection()
        {
            _item = null;
            GetComponent<Image>().sprite = _emptySprite;
        }

        private void showOrHideItems(bool show)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                _items[i].gameObject.SetActive(show);
            }
            _isShowingItems = show;
        }

        public void OnClick()
        {
            if (_item != null)
            {
                AddNewItem(_item);
            }
            ClearSelection();
            CloseOrOpen(false);
        }

        public void CloseOrOpen(bool forceClose)
        {
            
            if (forceClose)
            {
                _isShowingItems = true;
            }
            showOrHideItems(!_isShowingItems);
        }


    }
}
