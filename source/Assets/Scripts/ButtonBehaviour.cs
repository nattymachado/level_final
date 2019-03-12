using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public bool isActiveToMove = false;
    
     void Update()
    {
        
    }

    private void MoveButton()
    {
        transform.Rotate(0, 0, -180);
        Debug.Log("Apertou");
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
        CharacterBehaviour characterBehaviour = other.GetComponent<CharacterBehaviour>();
        if (characterBehaviour != null)
        {
            if (isActiveToMove)
            {
                MoveAndExecute();
            }

        }
    }


}
