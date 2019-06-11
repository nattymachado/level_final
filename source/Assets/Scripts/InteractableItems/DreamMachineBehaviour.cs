using UnityEngine;
using System.Collections;

public class DreamMachineBehaviour : InteractableItemBehaviour
{

    [SerializeField] private GameObject particleSystemPrefab;
    [SerializeField] private Transform position;

    private void Start()
    {
        executeWhenActivate = true;
    }

    protected override void ExecuteAction()
    {
        //Instantiate our one-off particle system
        GameObject explosionEffect = Instantiate(particleSystemPrefab, this.transform );
        explosionEffect.transform.position = transform.position;
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Explosion", false, false);
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Spark", false, false);
        //play it


        //destroy the particle system when its duration is up, right
        //it would play a second time.
        Destroy(explosionEffect, 10f);

    }

}
