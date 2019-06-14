using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLevelTreeBehaviour : InteractableItemBehaviour
{
    [SerializeField] CharacterBehaviour character;
    [SerializeField] string activatorItemName;
    [SerializeField] Animator treeAnimator;
    [SerializeField] DogLevelBowlBehaviour bowl;
    private bool activated = false;


    protected override void ExecuteAction(Collider other)
    {
        if (character && !activated && character.CheckInventaryObjectOnSelectedPosition(activatorItemName))
        {
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            ActivateTree();
        } 
    }

    private void ActivateTree()
    {
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Treeshake", false, false);
        activated = true;
        treeAnimator.SetTrigger("shake");
        bowl.Show();
    }

}
