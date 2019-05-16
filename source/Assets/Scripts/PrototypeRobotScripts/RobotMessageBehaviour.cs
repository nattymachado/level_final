using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace prototypeRobot
{

    public class RobotMessageBehaviour : MonoBehaviour
    {

        [SerializeField] private Material combination;
        [SerializeField] private Material error;

        private MeshRenderer meshRenderer;

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            Material[] mats = meshRenderer.materials;
            mats[1] = combination;
            meshRenderer.materials = mats;

            InvokeRepeating("ChangeImage", 1.0f, 1.0f);
        }

        void ChangeImage()
        {
           
                Material[] mats = meshRenderer.materials;
                if (meshRenderer.materials[1].name.Contains("combination"))
                {
                    mats[1] = error;
                }
                else
                {
                    mats[1] = combination;
                }
                meshRenderer.materials = mats;
            }
            
        }

       

    
}
