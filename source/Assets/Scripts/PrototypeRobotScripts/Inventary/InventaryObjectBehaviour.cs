using UnityEngine;
using UnityEngine.UI;

namespace prototypeRobot
{
    public class InventaryObjectBehaviour : MonoBehaviour
    {

        [SerializeField] public string Name;
        [SerializeField] public Image objectImage;
        [SerializeField] public Image objectImageCentrer;
        [SerializeField] public InventaryCenterBehaviour inventaryCenter;
        [SerializeField] public int Position;



        void OnTriggerEnter(Collider other)
        {
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            if (character != null)
            {
                IncludeItemOnInventary();

                gameObject.SetActive(false);
            }
        }

        private void IncludeItemOnInventary()
        {
            inventaryCenter.AddNewItem(this);
        }
    }
}
