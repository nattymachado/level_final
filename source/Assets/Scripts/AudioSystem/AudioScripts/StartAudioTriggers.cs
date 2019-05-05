using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioTriggers : MonoBehaviour
{
    //Trigger References
    [Header("Trigger References")]
    [SerializeField] private AudioClipTrigger[] _triggers;

    // Start is called before the first frame update
    private void Start()
    {
        if(_triggers != null)
        {
            foreach(AudioClipTrigger trigger in _triggers)
            {
                GameEvents.AudioEvents.TriggerSFX.SafeInvoke(trigger.trigger, trigger.loop);
            }
        }
    }
}
