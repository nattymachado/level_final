using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleRobot : InteractableItemBehaviour
{
    [SerializeField] private GameObject particleSystemPrefab;

    private void Start()
    {
        executeWhenActivate = true;
    }

    protected override void ExecuteAction()
    {
        Debug.Log("TEST");
        //Instantiate our one-off particle system
        GameObject explosionEffect = Instantiate(particleSystemPrefab, this.transform);
        explosionEffect.transform.position = transform.position;
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Explosion", false, false);
        //play it


        //destroy the particle system when its duration is up, right
        //it would play a second time.
        Destroy(explosionEffect, 10f);
    }
}
