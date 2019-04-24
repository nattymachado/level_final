using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class CardBox2Behaviour : InteractableItemBehaviour
    {
        [SerializeField] CharacterBehaviour character;
        [SerializeField] GameObject item;
        [SerializeField] float yPosition;
        [SerializeField] float speed;
        [SerializeField] string cardName;

        protected override void ExecuteAction(Collider other)
        {
            if (character && character.checkInventaryObjectOnSelectedPosition(cardName))
            {
                item.SetActive(true);
            }

        }

        private void Update()
        {
            if (item.activeSelf && item.transform.position.y < yPosition)
            {
                Vector3 target = new Vector3(item.transform.position.x, yPosition, item.transform.position.z);
                item.transform.position = Vector3.MoveTowards(item.transform.position, target, speed * Time.deltaTime);
            }
        }


    }
}
