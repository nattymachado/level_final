using UnityEngine;
using UnityEngine.UI;

namespace prototypeRobot
{
    public class InventaryBagButtonBehaviour : MonoBehaviour
    {

        [SerializeField] private InventaryCircleBehaviour inventary;
        [SerializeField] private InventaryReturnButtonBehaviour backButton;

        public void OnClick()
        {
            inventary.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
