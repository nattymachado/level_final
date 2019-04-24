using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace prototypeRobot
{
    public class InventaryCircleBehaviour : MonoBehaviour
    {
        [SerializeField] float rotSpeed = 6;
        [SerializeField] public List<InventaryItemBehaviour> InventaryPositions = new List<InventaryItemBehaviour>();
        [SerializeField] CharacterBehaviour character;
        [SerializeField] Sprite emptySprite;
        float positionCircle = 0;
        bool lastIsRight = false;
        private int nextItemPosition = 0;

        public void addNewItem(InventaryObjectBehaviour item)
        {
            for (int i = 0; i < InventaryPositions.Count; i++)
            {
                if (InventaryPositions[i].IsEmpty())
                {
                    InventaryPositions[i].AddItem(item);
                    break;
                }
            }
        }

        public void removeNewItem(int position)
        {
            InventaryPositions[position].RemoveItem();

        }
    }
}