using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonKeyBehaviour : ButtonBehaviour
{

    [SerializeField] private CharacterBehaviour character;
    [SerializeField] private GameObject gate;
    public override void Execute()
    {
        Debug.Log("Execute key");
        Debug.Log(character.inventary[0]);
        if (character.inventary.Exists(x => x == "Key"))
        {
            Destroy(gate);
        }
    }


}
