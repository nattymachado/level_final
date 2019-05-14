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
        private bool isOn = false;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _original = GetComponentInChildren<MeshRenderer>().material;
            isOn = false;
        }

        public void turnOn()
        {
            _original = colorMaterial;
            GetComponentInChildren<MeshRenderer>().material = colorMaterial;
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
                isRight = GetComponentInParent<ColorControllerBehaviour>().CheckColorPosition(color, GetComponentInChildren<MeshRenderer>());
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
