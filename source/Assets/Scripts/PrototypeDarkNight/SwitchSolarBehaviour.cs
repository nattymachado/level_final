using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSolarBehaviour : MonoBehaviour
{
    
    [SerializeField] PlanetPilarBehaviour pilar;
    [SerializeField] GameObject sun;
    [SerializeField] public GridBehaviour Grid;

    public bool isActiveToMove = false;

    void Update()
    {
        
    }

    private void MoveButton()
    {
        transform.Rotate(0, 0, -180);
        Debug.Log("Apertou");
    }

    public virtual void Execute(CharacterBehaviour2 character)
    {
        Debug.Log("Executar");

        pilar.RotateCameraToRight(character);
        
    }

    private void MoveAndExecute(CharacterBehaviour2 character)
    {
        if (pilar && sun.activeSelf)
        {
            MoveButton();
            Execute(character);
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
                MoveAndExecute(characterBehaviour);
            }

        }
    }


}
