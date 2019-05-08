using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class ShapeButtonBehaviour : InteractableItemBehaviour
    {

        [SerializeField] public GridBehaviour Grid;
        [SerializeField] public string shape;


        protected override void ExecuteAction(Collider other)
        {
            if (!GetComponent<MeshRenderer>().enabled)
            {
                GetComponentInParent<ShapeControllerBehaviour>().CheckShapePosition(shape, GetComponent<MeshRenderer>());
            }
        }

        public void Clear()
        {
            GetComponent<MeshRenderer>().enabled = false;
            SetActive(false);
        }

        
    }
}
