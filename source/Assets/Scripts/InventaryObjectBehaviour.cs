using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaryObjectBehaviour : MonoBehaviour
{
    public string Name;

    void OnTriggerEnter(Collider other)
    {
        CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
       if (character != null)
        {
            character.inventary.Add(name);
            Destroy(gameObject);
        }
    }

    


}
