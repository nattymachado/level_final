using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPilarBehaviour : MonoBehaviour
{
    
    [SerializeField] public GameObject Sun;
    [SerializeField] public GridBehaviour Grid;

    public bool isActiveToMove = false;

    void Update()
    {
        
    }

    private void MoveButton()
    {
        Sun.SetActive(true);
    }

    public virtual void Execute()
    {
        Debug.Log("Executar");
        
        
    }

    private void MoveAndExecute()
    {
        MoveButton();
        Execute();
        isActiveToMove = false;
    }

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
        if (characterBehaviour != null  && characterBehaviour.Inventary.Count > 0 
            && characterBehaviour.Inventary[characterBehaviour.SelectedItemPosition] != null 
            && characterBehaviour.Inventary[characterBehaviour.SelectedItemPosition].Name == "Sun")
        {
            if (isActiveToMove)
            {
                MoveAndExecute();
            }

        }
    }


}
