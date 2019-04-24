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
        [SerializeField] public GameObject card3;
        [SerializeField] public GameObject item1;
        private int position = 0;

        public void CheckColorPosition(string color, MeshRenderer colorMesh)
        {
            if (sequece[position] == color)
            {
                colorMesh.material = right;
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
                card3.SetActive(true);
                item1.SetActive(true);
            }
        }

 
        private IEnumerator WaitAndClear(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            ColorButtonBehaviour[] buttons = GetComponentsInChildren<ColorButtonBehaviour>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Clear();
            }
            position = 0;
           
        }


    }
}
