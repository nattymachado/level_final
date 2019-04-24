using UnityEngine;
using UnityEngine.UI;

namespace prototypeRobot
{
    public class InventaryReturnButtonBehaviour : MonoBehaviour
    {

        [SerializeField] private InventaryCircleBehaviour inventary;
        [SerializeField] private InventaryBagButtonBehaviour inventaryBagButton;

        public void OnClick()
        {
            inventary.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            inventaryBagButton.gameObject.SetActive(true);
        }
    }
}
