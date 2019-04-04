using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClientBehaviour : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        CheckCharacterToExecute(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckCharacterToExecute(other);

    }

    void CheckCharacterToExecute(Collider other)
    {
        CharacterBehaviour2 characterBehaviour = other.GetComponent<CharacterBehaviour2>();
        if (characterBehaviour != null)
        {
            if (characterBehaviour.Inventary.Count > 0 && characterBehaviour.Inventary[characterBehaviour.SelectedItemPosition] != null
                && characterBehaviour.Inventary[characterBehaviour.SelectedItemPosition].Name == "BrightSun")
            {
                characterBehaviour.Inventary[characterBehaviour.SelectedItemPosition].RemoveItemOnInventary();
                Debug.Log("End!!!");
                Application.Quit();
            }

        }
    }
}
