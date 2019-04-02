using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehaviour : MonoBehaviour
{
    
    [SerializeField] public GridBehaviour Grid;
    [SerializeField] public GameObject GateBar;

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
        
        Destroy(GateBar);
        GateBar = null;
        
    }

    private void MoveAndExecute()
    {
        if (GateBar)
        {
            MoveButton();
            Execute();
            isActiveToMove = false;
        }
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
        if (characterBehaviour != null)
        {
            if (isActiveToMove)
            {
                MoveAndExecute();
            }

        }
    }


}
