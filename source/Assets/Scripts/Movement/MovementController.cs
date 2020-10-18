﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovementController : MonoBehaviour
{

    private LayerMask _raycastMaskItem;
    [SerializeField] private CharacterBehaviour _character;
    [SerializeField] private Pointer pointer;
    private float updatePointerTimer = -1;
    private Vector3 positionToMove;
    private Animator _pointerAnimator;
    private bool _hasMoved;

    private void Awake()
    {
        _raycastMaskItem = LayerMask.GetMask(new string[] { "Interactable" });
        _pointerAnimator = pointer.GetComponent<Animator>();
    }


    private void PositionOnBoard(Vector3 position)
    {
        Debug.Log("testig");
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit[] hits = new RaycastHit[1];
        RaycastHit hit;

        if (InputController.IsPointOnBoard(position, out hit))
        {
            Debug.Log(hit.collider);
            MoveToPosition(hit.collider.GetComponent<GridBehaviour>(), hit.point);
        }
    }

    public void MoveToPosition(GridBehaviour grid, Vector3 point)
    {
        Debug.Log("testig");
        Node boardNode = grid.NodeFromWorldPosition(point);
        pointer.transform.position = new Vector3(boardNode.worldPosition.x, boardNode.worldPosition.y + grid.pointerPosition, boardNode.worldPosition.z);
        _pointerAnimator.SetTrigger("Clicked");
        Debug.Log(pointer.transform.position);
        _character.Move(pointer.transform.position);

        // trigger event
        GameEvents.LevelEvents.Moved.SafeInvoke();
    }

    public void ActiveItemOrMove(Vector3 position)
    {
        bool hasItem = ActiveItem(position);
        if (!hasItem)
        {
            Move(position);
        }
    }

    public void Move(Vector3 position)
    {
        PositionOnBoard(position);
    }

    public bool ActiveItem(Vector3 position)
    {
        bool activateItem = false;
        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit[] hits = new RaycastHit[1];
        Physics.RaycastNonAlloc(ray, hits, 500f, _raycastMaskItem);
        if (hits[0].collider != null)
        {
            InteractableItemBehaviour item = hits[0].collider.GetComponent<InteractableItemBehaviour>();
            if (item)
            {
                item.ActivateAndGo(true, _character);
                Vector3 point = hits[0].point;
                if (item.pointOnNavMesh != null && item.grid != null)
                {
                    MoveToPosition(item.grid, item.pointOnNavMesh.position);
                }
                else
                {
                    Move(position);
                }
                activateItem = true;

            }

        }
        return activateItem;
    }
}
