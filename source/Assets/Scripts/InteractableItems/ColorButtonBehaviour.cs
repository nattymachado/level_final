using UnityEngine;
using System.Collections;

namespace prototypeRobot
{
    public class ColorButtonBehaviour : InteractableItemBehaviour
    {

        [SerializeField] public GridBehaviour Grid;
        [SerializeField] public string color;


        protected override void ExecuteAction(Collider other)
        {
            if (!GetComponent<MeshRenderer>().enabled)
            {
                GetComponentInParent<ColorControllerBehaviour>().CheckColorPosition(color, GetComponent<MeshRenderer>());
            }
        }

        public void Clear()
        {
            GetComponent<MeshRenderer>().enabled = false;
            SetActive(false);
        }

        
    }
}
