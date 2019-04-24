using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class ShapeButtonBehaviour : MonoBehaviour
    {

        [SerializeField] public GridBehaviour Grid;
        [SerializeField] public string shape;

        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterBehaviour>() != null)
            {
                ExecuteAction(other);
            }
            

        }

        protected void ExecuteAction(Collider other)
        {
            if (!GetComponent<MeshRenderer>().enabled)
            {
                GetComponentInParent<ShapeControllerBehaviour>().CheckShapePosition(shape, GetComponent<MeshRenderer>());
            }
        }

        public void Clear()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        
    }
}
