using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleRobot : InteractableItemBehaviour
{
    [SerializeField] private GameObject particleSystemPrefab;
    [SerializeField] private GameObject character;
    [SerializeField] private float triggerDistance = 6f;

    private void Start()
    {
        executeWhenActivate = true;
    }

    protected override void ExecuteAction()
    {
        if(character.transform.position != null && Vector3.Distance(character.transform.position, this.transform.position) <= triggerDistance)
        {
            //Instantiate our one-off particle system
            GameObject explosionEffect = Instantiate(particleSystemPrefab, this.transform);
            explosionEffect.transform.position = transform.position;
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Explosion", false, false);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("RobotDistress", false, false);
            Destroy(explosionEffect, 10f);
        }
    }
}
