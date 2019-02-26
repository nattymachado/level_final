using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
    Right,
    Left,
    Up,
    Down
}
public class RotateButtonBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Direction direction = Direction.Right;
    bool pressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    void Update()
    {
        if (!pressed)
            return;
        CameraBehaviour cameraBehaviour = mainCamera.GetComponent<CameraBehaviour>();
        switch (direction)
        {
            case Direction.Right:
                cameraBehaviour.RotateCameraToRight(90);
                break;
            case Direction.Left:
                cameraBehaviour.RotateCameraToLeft(90);
                break;
        }
        
        
    }
}
