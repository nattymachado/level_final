using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class ColorButtonBehaviour : InteractableItemBehaviour
    {

        [SerializeField] public string color;
        [SerializeField] public bool isRight;
        [SerializeField] public Material colorMaterial;
        private Animator _animator;
        private Material _original;
        private Vector3 _target;
        private bool isOn = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _original = GetComponentInChildren<MeshRenderer>().material;
            _target = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            isOn = false;
        }

        public void turnOn()
        {
            isOn = true;
        }


        protected override void ExecuteAction(Collider other)
        {
            if (!isOn)
            {
                return;
            }
            if (!isRight)
            {
                _animator.SetBool("isPressed", true);
                GetComponentInChildren<MeshRenderer>().material = colorMaterial;
                isRight = GetComponentInParent<ColorControllerBehaviour>().CheckColorPosition(color, GetComponentInChildren<MeshRenderer>());
            }
        }

        private void Update()
        {
            if (isOn)
            {
                float step = 0.5f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _target, step);
            }
        }

        public void Clear()
        {
            _animator.SetBool("isPressed", false);
            GetComponentInChildren<MeshRenderer>().material = _original;
            SetActive(false);
            isRight = false;
        }

        
    }
}
