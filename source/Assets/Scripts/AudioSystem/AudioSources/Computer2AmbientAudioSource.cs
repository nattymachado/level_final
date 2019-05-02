using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer2AmbientAudioSource : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.RobotSceneAudioEvents.InsertedKeycardGreen += ActivateAmbientSound;
    }

    private void OnDestroy()
    {
        GameEvents.RobotSceneAudioEvents.InsertedKeycardGreen -= ActivateAmbientSound;
    }

    //Activate Ambient Sound Effect
    private void ActivateAmbientSound()
    {
        AmbientAudioSource reference = this.GetComponent<AmbientAudioSource>();
        if (reference != null) reference.enabled = true;
    }
}
