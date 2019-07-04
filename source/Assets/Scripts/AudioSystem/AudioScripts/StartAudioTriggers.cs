using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioTriggers : MonoBehaviour
{
    //Trigger References
    [Header("SFX Trigger References")]
    [SerializeField] private AudioClipTrigger[] _SFXTriggers;
    [Header("BGM Trigger References")]
    [SerializeField] private AudioClipTrigger[] _BGMTriggers;

    //Awake
    private void Awake()
    {
        GameEvents.GameStateEvents.BGMSceneLoaded += TriggerBGM;
    }

    // Start is called before the first frame update
    private void Start()
    {
        TriggerSFX();
    }

    private void OnDestroy()
    {
        GameEvents.GameStateEvents.BGMSceneLoaded -= TriggerBGM;
    }

    private void TriggerSFX()
    {
        if (_SFXTriggers != null)
        {
            foreach (AudioClipTrigger trigger in _SFXTriggers)
            {
                GameEvents.AudioEvents.TriggerSFX.SafeInvoke(trigger.trigger, trigger.loop, false);
            }
        }
    }

    private void TriggerBGM()
    {
        if (_BGMTriggers != null)
        {
            foreach (AudioClipTrigger trigger in _BGMTriggers)
            {
                GameEvents.AudioEvents.PlayBGM.SafeInvoke(trigger.trigger);
            }
        }
    }
}
