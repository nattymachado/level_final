using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientBehaviour : InteractableItemBehaviour
{
    [SerializeField] public string FirstScene;
    [SerializeField] public GridBehaviour Grid;
    [SerializeField] public string itemName;
    [SerializeField] public GameEnums.PatientEnum _patient;


    protected override void ExecuteAction(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (itemName == "" || (character && character.CheckInventaryObjectOnSelectedPosition(itemName)))
        {
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
            GameEvents.UIEvents.OpenPatientRecord.SafeInvoke(_patient);
        }
    }
}


