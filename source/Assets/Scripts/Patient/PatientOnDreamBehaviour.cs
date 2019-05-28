using UnityEngine;
using UnityEngine.SceneManagement;

public class PatientOnDreamBehaviour : InteractableItemBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] private GameEnums.LevelEnum _patientLevel;

    protected override void ExecuteAction(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
        if (character && character.CheckInventaryObjectOnSelectedPosition(itemName))
        {
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
            GameStatus.Instance.SetLastLevel(_patientLevel);
            SceneChanger.Instance.ChangeToScene("hospital");
        }
    }
}


