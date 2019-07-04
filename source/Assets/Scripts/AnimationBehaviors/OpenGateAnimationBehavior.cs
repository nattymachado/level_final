using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGateAnimationBehavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.gameObject.name.Equals("gateParent")) GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Gate1", false, false);
        else if (animator.gameObject.name.Equals("gateParent2")) GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Gate2", false, false);
    }
}
