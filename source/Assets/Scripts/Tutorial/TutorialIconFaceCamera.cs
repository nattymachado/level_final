using UnityEngine;

public class TutorialIconFaceCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x - Camera.main.transform.position.x,transform.position.y,transform.position.z - Camera.main.transform.position.z));
    }
}
