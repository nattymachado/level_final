using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{

    [SerializeField] private Animator cameraAnimator;
    private bool isAnimating = true;
    private Action approachCallback = null;
    

    public void Approach(Action callback){
        approachCallback = callback;
        cameraAnimator.SetTrigger("approach");
    }

    public void FinishApproach(){
        isAnimating = false;

        if(approachCallback!=null) approachCallback.Invoke();
    }
}
