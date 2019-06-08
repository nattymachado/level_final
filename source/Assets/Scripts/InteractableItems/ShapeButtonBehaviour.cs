using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class ShapeButtonBehaviour : InteractableItemBehaviour
    {

        [SerializeField] public string shape;
        [SerializeField] public bool isRight;
        private Animator _animator;
        private Material _original;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _original = GetComponentInChildren<MeshRenderer>().material;
        }


        protected override void ExecuteAction(Collider other)
        {
            if (isRight) return;
            else if (GetComponentInParent<ShapeControllerBehaviour>().isActive)
            {
                _animator.SetBool("isPressed", true);
                isRight = GetComponentInParent<ShapeControllerBehaviour>().CheckShapePosition(shape, GetComponentInChildren<MeshRenderer>());
            }
        }

        public void Clear()
        {
            _animator.SetBool("isPressed", false);
            GetComponentInChildren<MeshRenderer>().material = _original;
            isRight = false;
            SetActive(false);
        }

        
    }
}
