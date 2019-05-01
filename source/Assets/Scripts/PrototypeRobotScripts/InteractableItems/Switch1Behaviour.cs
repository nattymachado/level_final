using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class Switch1Behaviour : InteractableItemBehaviour
    {
        [SerializeField] GameObject gate;
        [SerializeField] float yPosition;
        [SerializeField] float speed;
        private bool _canOpenGate = false;

        protected override void ExecuteAction(Collider other)
        {
            if (!_canOpenGate)
            {
                transform.Rotate(0, 0, -180);
                _canOpenGate = true;
            }
            
        }

        private void Update()
        {
            if (_canOpenGate && gate.transform.position.y > yPosition)
            {
                Vector3 target = new Vector3(gate.transform.position.x, yPosition, gate.transform.position.z);
                gate.transform.position = Vector3.MoveTowards(gate.transform.position, target, speed * Time.deltaTime);
            } else if (gate.transform.position.y <= yPosition)
            {
                gate.SetActive(false);
            }
        }


    }
}
