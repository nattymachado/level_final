using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace prototypeRobot
{
    public class ShapeControllerBehaviour : MonoBehaviour
    {

        [SerializeField] public string[] sequece = new string[4];
        [SerializeField] public Material right;
        [SerializeField] public Material wrong;
        [SerializeField] public GameObject card3;
        //[SerializeField] public GameObject item1;
        private int position = 0;

        public void CheckShapePosition(string shape, MeshRenderer shapeMesh)
        {
            if (sequece[position] == shape)
            {
                GameEvents.RobotSceneAudioEvents.Puzzle1ButtonClick.SafeInvoke();
                shapeMesh.material = right;
                position += 1;
            } else
            {
                shapeMesh.material = wrong;
                IEnumerator  coroutine = WaitAndClear(1.0f);
                StartCoroutine(coroutine);
            }
            shapeMesh.enabled = true;
            if (position == sequece.Length)
            {
                GameEvents.RobotSceneAudioEvents.SuccessfulPuzzle1.SafeInvoke();
                card3.SetActive(true);
                //item1.SetActive(true);
            }
        }

 
        private IEnumerator WaitAndClear(float waitTime)
        {
            GameEvents.RobotSceneAudioEvents.FailedPuzzle1.SafeInvoke();
            yield return new WaitForSeconds(waitTime);
            ShapeButtonBehaviour[] buttons = GetComponentsInChildren<ShapeButtonBehaviour>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Clear();
            }
            position = 0;
           
        }


    }
}
