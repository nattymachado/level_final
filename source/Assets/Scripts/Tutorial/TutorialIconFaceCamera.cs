using UnityEngine;

[ExecuteInEditMode]
public class TutorialIconFaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 invertedCameraPosition = transform.position - (Camera.main.transform.position - transform.position);
        transform.LookAt(new Vector3(invertedCameraPosition.x, transform.position.y, invertedCameraPosition.z));
    }
}
