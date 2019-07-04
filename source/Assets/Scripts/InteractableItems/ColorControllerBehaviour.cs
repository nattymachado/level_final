using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace prototypeRobot
{
    public class ColorControllerBehaviour : MonoBehaviour
    {
        [HideInInspector] public bool isActive = true;
        [SerializeField] public string[] sequece = new string[4];
        [SerializeField] public Material wrongColor;
        [SerializeField] public GameObject card;
        [SerializeField] public Animator gateAnimator;
        [SerializeField] public GameObject navMeshWithoutButtons;
        [SerializeField] public GameObject navMeshWithButtons;

        private int position = 0;

        public bool CheckColorPosition(string color, ColorButtonBehaviour colorButton)
        {
            bool isRight = false;
            if (sequece[position] == color)
            {
                GameEvents.AudioEvents.TriggerRandomSFX.SafeInvoke("ButtonClick", false, false);
                colorButton.SetRightColor();
                isRight = true;
                position += 1;
            }
            else
            {
                isActive = false;
                colorButton.SetWrongColor(wrongColor);
                IEnumerator coroutine = WaitAndClear(1.0f);
                StartCoroutine(coroutine);
            }
            if (position == sequece.Length)
            {
                StartCoroutine(nameof(DropCard));
            }
            return isRight;
        }

        private IEnumerator DropCard()
        {
            yield return new WaitForSeconds(0.5f);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Drop_Key_Blue", false, true);
            card.SetActive(true);
            gateAnimator.SetBool("isOpen", true);
        }

        private IEnumerator WaitAndClear(float waitTime)
        {
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Does-not-compute", false, true);
            yield return new WaitForSeconds(waitTime);
            ColorButtonBehaviour[] buttons = GetComponentsInChildren<ColorButtonBehaviour>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Clear();
            }
            position = 0;
            isActive = true;
        }

        public void turnOnColors()
        {
            navMeshWithButtons.SetActive(true);
            navMeshWithoutButtons.SetActive(false);
            ColorButtonBehaviour[] buttons = GetComponentsInChildren<ColorButtonBehaviour>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].turnOn();
            }
            position = 0;

        }


    }
}
