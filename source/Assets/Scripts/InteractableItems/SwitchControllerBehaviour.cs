using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace prototypeRobot
{
    public class SwitchControllerBehaviour : MonoBehaviour
    {

        [SerializeField] public int[] sequece = new int[4];
        [SerializeField] public MeshRenderer[] lights = new MeshRenderer[4];
        [SerializeField] public Material right;
        [SerializeField] public Material wrong;
        [SerializeField] public Material off;
        [SerializeField] public Animator gateAnimator;
        [SerializeField] public GameObject item;
        [SerializeField] public InitRampBehaviour initRamp;
        private int _position = 0;

        public void CheckSwitchPosition(int switchId)
        {
            if (sequece[_position] == switchId)
            {
                lights[_position].material = right;
                _position += 1;
            } else
            {
                lights[_position].material = wrong;
                IEnumerator  coroutine = WaitAndClear(1.0f);
                StartCoroutine(coroutine);
            }
            if (_position == sequece.Length)
            {
                gateAnimator.SetBool("isOpen", true);
                item.SetActive(true);
            }
        }


        private IEnumerator WaitAndClear(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SequenceSwitchBehaviour[] buttons = GetComponentsInChildren<SequenceSwitchBehaviour>();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Clear();
                lights[i].material = off;
            }
            _position = 0;

        }


    }
}
