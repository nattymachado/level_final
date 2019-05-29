using UnityEngine;
using UnityEngine.SceneManagement;

public class PatientOnDreamBehaviour : InteractableItemBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] private GameEnums.LevelEnum _patientLevel;
    [SerializeField] private VictoryCanvasController victoryCanvas;
    
    private void Awake(){
        // garantia de preenchimento
        if (victoryCanvas == null) Debug.LogError("victory canvas empty on patient");
    }

    protected override void ExecuteAction(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (character && character.CheckInventaryObjectOnSelectedPosition(itemName))
        {
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
            GameStatus.Instance.SetLastLevel(_patientLevel);
            victoryCanvas.Open();
        }
    }
}


