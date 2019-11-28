using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleRobot : InteractableItemBehaviour
{
    [SerializeField] private GameObject particleSystemPrefab;
    [SerializeField] private GameObject character;
    [SerializeField] private Animator animator;
    [SerializeField] private float triggerDistance = 6f;
    private bool _isDestroyed = false;

    private void Start()
    {
        executeWhenActivate = true;
    }

    protected override void ExecuteAction()
    {
        if(!_isDestroyed && character.transform.position != null && Vector3.Distance(character.transform.position, this.transform.position) <= triggerDistance)
        {
            //Instantiate our one-off particle system
            _isDestroyed = true;
            GameObject explosionEffect = Instantiate(particleSystemPrefab, this.transform);
            explosionEffect.transform.position = transform.position;
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Explosion", false, false);
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("RobotDistress", false, false);
            GameEvents.Interactables.LittleRobot.SafeInvoke();
            animator.SetTrigger("Destroy");
            Destroy(explosionEffect, 10f);

        }
    }
}
