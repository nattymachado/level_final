using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PatientEndPositionBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterBehaviour>() == null)
        {
            other.gameObject.SetActive(false);
            GameStatus.Instance.IncludeDeactivatePatient(other.transform.parent.GetComponent<PatientOnHospitalBehaviour>().patient);
        }
    }
}


