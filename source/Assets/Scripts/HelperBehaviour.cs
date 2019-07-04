using UnityEngine;
using UnityEngine.SceneManagement;

public class HelperBehaviour : InteractableItemBehaviour
{


    protected override void ExecuteAction(CharacterBehaviour character)
    {
        SetActive(false);
        GameEvents.UIEvents.OpenTutorialDialogBox.SafeInvoke();
    }
}


