using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class LightGateBehaviour : MonoBehaviour
{
    [SerializeField] List<LightGateController.LightIdsEnum> lightIds;
    Animator _gateAnimator;

    private void Start()
    {
        _gateAnimator = GetComponent<Animator>();
    }

    public IEnumerator OpenGate()
    {
        yield return new WaitForSeconds(0.1f);
        _gateAnimator.SetBool("Open", true);
       
    }

    public IEnumerator CloseGate()
    {
        yield return new WaitForSeconds(0.1f);
        _gateAnimator.SetBool("Open", false);

    }

    public void CheckToCloseOrOpen(List<LightGateController.LightIdsEnum> activeLights)
    {
        var hasInactiveLight = lightIds.Any(lightId => !activeLights.Contains(lightId));
        if (hasInactiveLight)
        {
            StartCoroutine(CloseGate());
        } else
        {
            StartCoroutine(OpenGate());
        }
    }


}
