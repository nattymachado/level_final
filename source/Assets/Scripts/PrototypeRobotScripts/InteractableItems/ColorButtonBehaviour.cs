using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class ColorButtonBehaviour : MonoBehaviour
    {

        [SerializeField] public GridBehaviour Grid;
        [SerializeField] public string color;

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
                GetComponentInParent<ColorControllerBehaviour>().CheckColorPosition(color, GetComponent<MeshRenderer>());
            }
        }

        public void Clear()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        
    }
}
