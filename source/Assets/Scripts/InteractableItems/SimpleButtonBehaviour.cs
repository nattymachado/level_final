using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class SimpleButtonBehaviour : InteractableItemBehaviour
    {

        private Animator _animator;
        [SerializeField] public GameObject card;
        private bool isOn = false;
        private bool isUsed = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }


        protected override void ExecuteAction(CharacterBehaviour character)
        {
            if (isOn)
                return;
            isOn = true;
            
            _animator.SetBool("isPressed", true);
            ShowCard();
        }

        protected void ShowCard()
        {
            if (isUsed)
                return;
            GameEvents.AudioEvents.TriggerRandomSFX.SafeInvoke("ButtonClick", false, false);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Drop_Key_Blue", false, true);
            isUsed = true;
            card.SetActive(true);
        }


        /*void OnTriggerExit(Collider other)
        {
            _animator.SetBool("isPressed", false);
            isOn = false;

        }*/

    }
}
