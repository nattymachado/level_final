using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace prototypeRobot
{
    public class InventaryItemBehaviour : MonoBehaviour
    {
        [SerializeField] public InventaryObjectBehaviour Item;
        [SerializeField] private Sprite _emptySprite;
        [SerializeField] private InventaryCenterBehaviour _inventaryCenter;

        public void AddItem(InventaryObjectBehaviour item)
        {
            this.Item = item;
            GetComponent<Image>().sprite = Item.objectImage.sprite;
        }

        public void RemoveItem()
        {
            Item = null;
            GetComponent<Image>().sprite = _emptySprite;
        }

        public bool IsEmpty()
        {
            return Item == null;
        }

        public void OnClick() => _inventaryCenter.SelectItem(Item);

    }
}
