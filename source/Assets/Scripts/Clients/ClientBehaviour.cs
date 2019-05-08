using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientBehaviour : InteractableItemBehaviour
{
    [SerializeField] public string FirstScene;
    [SerializeField] public GridBehaviour Grid;
    [SerializeField] public string itemName;


    protected override void ExecuteAction(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (itemName == "" || (character && character.CheckInventaryObjectOnSelectedPosition(itemName)))
        {
            SceneManager.LoadScene(FirstScene, LoadSceneMode.Single);
        }
    }
}


