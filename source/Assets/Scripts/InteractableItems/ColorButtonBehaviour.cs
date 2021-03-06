﻿using UnityEngine;
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
            _target = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            isOn = false;
        }

        public void turnOn()
        {
            isOn = true;
        }


        protected override void ExecuteAction(CharacterBehaviour character)
        {
            if (!isOn) return;
            else if (!isRight && GetComponentInParent<ColorControllerBehaviour>().isActive)
            {
                _animator.SetBool("isPressed", true);
                GetComponentInChildren<MeshRenderer>().material = colorMaterial;
                isRight = GetComponentInParent<ColorControllerBehaviour>().CheckColorPosition(color, this);
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

        public void SetWrongColor(Material wrongColor)
        {
            GetComponentInChildren<MeshRenderer>().material = wrongColor;
        }

        public void SetRightColor()
        {
            GetComponentInChildren<MeshRenderer>().material = colorMaterial;
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
