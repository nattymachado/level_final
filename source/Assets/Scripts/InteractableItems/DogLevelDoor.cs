using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLevelDoor : MonoBehaviour
{
    private bool opened = false;
    [SerializeField] private string audioTrigger;
    [SerializeField] private Animator animator;

    private void Open()
    {
        animator.SetBool("opened",true);
        opened = true;
        // Debug.Log("open");
    }

    private void Close()
    {
        animator.SetBool("opened",false);
        opened = false;
        // Debug.Log("close");
    }

    public void Toggle()
    {
        if(opened) Close(); else Open();
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke(audioTrigger, false, false);
    }

    public void ForceOpen()
    {
        Open();
    }

    public void ForceClose()
    {
        Close();
    }
}
