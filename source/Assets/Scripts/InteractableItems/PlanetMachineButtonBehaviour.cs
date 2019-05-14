using UnityEngine;
using System.Collections;


public class PlanetMachineButtonBehaviour : InteractableItemBehaviour
{
    [SerializeField] public GameObject planet;
    [SerializeField] GameObject support;
    [SerializeField] float speed;
    [SerializeField] public bool isOn = false;
    [SerializeField] public PlanetMachineBehaviour planetMachine;
    [SerializeField] public Transform rotatePoint;
    [SerializeField] public int position;
    private bool _canRotate = false;


    protected override void ExecuteAction(Collider other)
    {
        if (!isOn)
        {
            return;
        }
        _canRotate = true;



    }

    private void Update()
    {
        if (!isOn)
        {
            return;
        }
        //base.Shine();
        if (_canRotate && planet != null)
        {
            planet.transform.RotateAround(rotatePoint.position, Vector3.up, speed);
            support.transform.RotateAround(rotatePoint.position, Vector3.up, speed);
            _canRotate = false;
            SetActive(false);
            position++;
            if (position > 4)
            {
                position = 1;
            }
            planetMachine.CheckPlanets();
        }
    }


}

