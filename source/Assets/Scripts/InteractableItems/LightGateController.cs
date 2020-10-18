using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LightGateController : MonoBehaviour
{

    [SerializeField] private List<LightGateBehaviour> gates;
    [SerializeField] private List<LightIdsEnum> activeLights;


    public enum LightIdsEnum
    {
        RedInit1,
        Red1,
        Red2,
        Red3,
        Red4,
        BlueInit1,
        BlueInit2,
        Blue1,
        Blue2,
        Blue3,
        Blue4,
        Green1,
        Green2,
        Green3
    }

    private void Start()
    {
        activeLights = new List<LightGateController.LightIdsEnum>();
    }

    public void AddActiveLight(LightGateController.LightIdsEnum activeLight)
    {
        Debug.Log("Add light");
        activeLights.Add(activeLight);
        CheckLightsAndGates();
    }

    public void RemoveInactiveLight(LightGateController.LightIdsEnum inactiveLight)
    {
        Debug.Log("Removing light");
        activeLights.Remove(inactiveLight);
        CheckLightsAndGates();
    }

    public void ChangeLight(LightGateController.LightIdsEnum activeLight, LightGateController.LightIdsEnum inactiveLight)
    {
        Debug.Log("Changinig light");
        activeLights.Remove(inactiveLight);
        activeLights.Add(activeLight);
        CheckLightsAndGates();
    }

    private void CheckLightsAndGates()
    {
        Debug.Log("Checking gates");
        foreach (LightGateBehaviour gate in gates)
        {
            gate.CheckToCloseOrOpen(activeLights);
        }
    }
}
