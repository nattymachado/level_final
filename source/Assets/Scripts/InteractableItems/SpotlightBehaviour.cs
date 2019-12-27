using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpotlightBehaviour : InteractableItemBehaviour
{

    [SerializeField] string lightName;
    [SerializeField] List<GameObject> _lights;
    [SerializeField] List<LightGateBehaviour> _gates;

    protected override void ExecuteAction(CharacterBehaviour character)
    {
        if (character && character.CheckInventaryObjectOnSelectedPosition(lightName))
        {
            

            for (int i = 0; i < _lights.Count; i++)
            {
                _lights[i].SetActive(true);
            }

            for (int i = 0; i < _gates.Count; i++)
            {
                Debug.Log("Executing 2");
                StartCoroutine(_gates[i].OpenGate());
            }
        }
    }

   

}
