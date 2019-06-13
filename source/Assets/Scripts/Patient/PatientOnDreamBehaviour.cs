using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PatientOnDreamBehaviour : InteractableItemBehaviour
{
    [SerializeField] private GameEnums.LevelEnum _patientLevel;
    [SerializeField] private VictoryCanvasController victoryCanvas;
    private bool specialItemIsUsed = false;
    
    private void Awake(){
        // garantia de preenchimento
        if (victoryCanvas == null) Debug.LogError("victory canvas empty on patient");
    }

    protected override void ExecuteAction(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (character && character.CheckIfSpecialIsActivated() && !specialItemIsUsed)
        {
            specialItemIsUsed = true;
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            // wait form reach destination
            SpecialCompleteItem specialCompleteItem = character.specialCompleteItem.GetComponent<SpecialCompleteItem>();
            specialCompleteItem.GetComponent<Animator>().SetTrigger("GoToPatient");
            StartCoroutine(WaitForSpacialItemGoToPacient(specialCompleteItem));

        }
    }

    private IEnumerator WaitForSpacialItemGoToPacient(SpecialCompleteItem specialCompleteItem)
    {
        while (specialCompleteItem.specialItemObject.activeSelf)
        {
            yield return null;
        }
        OpenVictoryCanvas();
    }

    private void OpenVictoryCanvas()
    {
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
        GameStatus.Instance.SetLastLevel(_patientLevel);
        victoryCanvas.Open();
    }
}


