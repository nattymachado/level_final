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
        [SerializeField] public GameObject gate;
        [SerializeField] float yPosition;
        [SerializeField] float speed;
        private bool _canOpenGate = false;
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
                _canOpenGate = true;
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

        private void Update()
       {
            if (_canOpenGate && gate.transform.position.y > yPosition)
            {
                Vector3 target = new Vector3(gate.transform.position.x, yPosition, gate.transform.position.z);
                gate.transform.position = Vector3.MoveTowards(gate.transform.position, target, speed * Time.deltaTime);
            }
        }


    }
}
