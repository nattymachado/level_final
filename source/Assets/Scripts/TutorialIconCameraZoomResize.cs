using UnityEngine;

[ExecuteInEditMode]
public class TutorialIconCameraZoomResize : MonoBehaviour
{
    private Vector3 initialLocalScale;
    private float initialCameraFov;
    [SerializeField] float fovToScale;

    void Start()
    {
        initialLocalScale = transform.localScale;
        initialCameraFov = Camera.main.fieldOfView;
    }

    void LateUpdate()
    {
        float currentFov = Camera.main.fieldOfView;
        float fovDiff = currentFov - initialCameraFov;

        Vector3 newLocalScale = initialLocalScale + Vector3.one * fovDiff * fovToScale;
        transform.localScale = newLocalScale;
    }
}
