using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatformBehaviour : ButtonBehaviour
{

    [SerializeField] private PlatformBehaviour platform;
    public override void Execute()
    {
        Debug.Log("Execute");   
        platform.isActiveToMove = true;
    }


}
