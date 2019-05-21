using UnityEngine;
using UnityEngine.SceneManagement;

public class HelperBehaviour : InteractableItemBehaviour
{


    protected override void ExecuteAction(Collider other)
    {
        SetActive(false);
        GameEvents.UIEvents.OpenTutorialDialogBox.SafeInvoke();
    }
}


