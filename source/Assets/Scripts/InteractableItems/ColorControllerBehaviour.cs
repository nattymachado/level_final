using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace prototypeRobot
{
    public class ColorControllerBehaviour : MonoBehaviour
    {

        [SerializeField] public string[] sequece = new string[4];
        [SerializeField] public Material right;
        [SerializeField] public Material wrong;
        [SerializeField] public GameObject card;
        [SerializeField] public Animator gateAnimator;
        private int position = 0;

        public bool CheckColorPosition(string color, MeshRenderer colorMesh)
        {
            bool isRight = false;
            if (sequece[position] == color)
            {
                GameEvents.AudioEvents.TriggerRandomSFX.SafeInvoke("ButtonClick", false);
                colorMesh.material = right;
                isRight = true;
                position += 1;
            } else
            {
                colorMesh.material = wrong;
                IEnumerator  coroutine = WaitAndClear(1.0f);
                StartCoroutine(coroutine);
            }
            colorMesh.enabled = true;
            if (position == sequece.Length)
            {
                GameEvents.AudioEvents.TriggerSFX.SafeInvoke("GenerateKeycard", false);
                card.SetActive(true);
                gateAnimator.SetBool("isOpen", true);
            }
            return isRight;
        }

 
        private IEnumerator WaitAndClear(float waitTime)
        {
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Does-not-compute", false);
            yield return new WaitForSeconds(waitTime);
            ColorButtonBehaviour[] buttons = GetComponentsInChildren<ColorButtonBehaviour>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Clear();
            }
            position = 0;
           
        }

        public void turnOnColors()
        {
            ColorButtonBehaviour[] buttons = GetComponentsInChildren<ColorButtonBehaviour>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].turnOn();
            }
            position = 0;

        }


    }
}
