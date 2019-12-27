using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class LightGateBehaviour : MonoBehaviour
{
    [SerializeField] List<GameObject> _lights;
    Animator _gateAnimator;

    private void Start()
    {
        _gateAnimator = GetComponent<Animator>();
    }

    public IEnumerator OpenGate()
    {
        if (!(_lights.Any(i => i.activeSelf == false))) {
            yield return new WaitForSeconds(0.1f);
            _gateAnimator.SetBool("Open", true);
        }
       
    }


}
