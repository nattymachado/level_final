using UnityEngine;
using UnityEngine.SceneManagement;

namespace prototypeRobot
{
    public class ClientBehaviour : MonoBehaviour
    {
        [SerializeField] public string FirstScene;
        [SerializeField] public GridBehaviour Grid;
        [SerializeField] public string  itemName;
        private bool isActived = false;



        void OnTriggerEnter(Collider other)
        {
            CheckCharacterToExecute(other);
        }

        void OnTriggerStay(Collider other)
        {
            CheckCharacterToExecute(other);
        }

        void DoAction(CharacterBehaviour character)
        {
            if (itemName == "" || (character && character.checkInventaryObjectOnSelectedPosition(itemName))) {
                SceneManager.LoadScene(FirstScene, LoadSceneMode.Single);
            }
        }

        public void Active()
        {
            isActived = true;
        }

        void CheckCharacterToExecute(Collider other)
        {
            CharacterBehaviour characterBehaviour = other.GetComponent<CharacterBehaviour>();
            if (characterBehaviour != null)
            {
                if (isActived)
                {
                    DoAction(characterBehaviour);
                    isActived = false;
                }

            }
        }
    }
}

