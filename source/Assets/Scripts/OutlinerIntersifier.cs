using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlinerIntersifier : MonoBehaviour
{
    public float minDistance = 3;
    public int subdivisions = 2;
    public float frequencyIntensifier = 10;
    public float blinkSizeMultiplier = 0.5f;
    public float thicknessIntensifier = 0.5f;

    private float frequencyStart;
    private float blinkSizeStart;
    private float thicknessStart;

    private Renderer objectRenderer;
    private bool isIntensifying = false;
    private float currentSubdivision = -1;

    private void OnDrawGizmos()
    {
        Color gizmoColor = Color.red;
        gizmoColor.a = 0.2f;
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, minDistance);
    }

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        frequencyStart = objectRenderer.material.GetFloat("_BlinkFrequency");
        blinkSizeStart = objectRenderer.material.GetFloat("_BlinkRisezer");
        thicknessStart = objectRenderer.material.GetFloat("_OutlineThickness");
    }


    private void Update()
    {
        foreach (var item in Physics.OverlapSphere(transform.position, minDistance))
        {
            if (item.gameObject.GetComponent<CharacterBehaviour>() == null) continue;
            float distance = Mathf.Abs(Vector3.Distance(item.transform.position, transform.position));
            if (distance > minDistance) continue;
            float eachSubdivisionDistance = minDistance / subdivisions;
            float subdivision = Mathf.Floor(distance / eachSubdivisionDistance);
            if (subdivision != currentSubdivision)
            {
                currentSubdivision = subdivision;
                float intensity = 1 - currentSubdivision / subdivisions;
                Intensify(intensity);
                isIntensifying = true;
            }
            return;
        }

        if (isIntensifying)
        { 
            Intensify(0);
            isIntensifying = false;
            currentSubdivision = -1;
        }
    }

    private void Intensify(float intensity)
    {
        objectRenderer.material.SetFloat("_BlinkFrequency", frequencyStart * (1 + intensity * frequencyIntensifier));
        objectRenderer.material.SetFloat("_BlinkRisezer", blinkSizeStart * (1 + intensity * blinkSizeMultiplier));
        objectRenderer.material.SetFloat("_OutlineThickness", thicknessStart * (1 + intensity * thicknessIntensifier));
    }
}
