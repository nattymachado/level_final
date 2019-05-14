using UnityEngine;
using UnityEngine.SceneManagement;

public class PacientOnHospitalBehaviour : InteractableItemBehaviour
{
    [SerializeField] public GameEnums.PatientEnum _patient;


    protected override void ExecuteAction(Collider other)
    { 
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.Victory);
        GameEvents.UIEvents.OpenPatientRecord.SafeInvoke(_patient);
    }
}


