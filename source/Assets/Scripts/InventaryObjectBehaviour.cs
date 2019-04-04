using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaryObjectBehaviour : MonoBehaviour
{
    public string Name;

    void OnTriggerEnter(Collider other)
    {
        CharacterBehaviour2 character = other.GetComponent<CharacterBehaviour2>();
       if (character != null)
        {
            //character.Inventary.Add(Name);
            Destroy(gameObject);
        }
    }

    


}
