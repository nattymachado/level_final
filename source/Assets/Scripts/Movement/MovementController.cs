using System.Collections;
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
    private bool _hasMoved;

    private void Awake()
    {
        _raycastMaskItem = LayerMask.GetMask(new string[] { "Interactable" });
    }


    private void PositionOnBoard(Vector3 position)
    {   
        RaycastHit hit;
        if ( InputController.IsPointOnBoard(position, out hit) ){
            GridBehaviour grid = hit.collider.GetComponent<GridBehaviour>();
            Node boardNode = grid.NodeFromWorldPosition(hit.point);
            pointer.transform.position = new Vector3(boardNode.worldPosition.x, grid.transform.position.y + 0.35f, boardNode.worldPosition.z);
        }
    }

    public void Move(Vector3 position)
    {
        PositionOnBoard(position);
        _character.Move(pointer.transform.position);
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
                item.SetActive(true);
                activateItem = true;
            }

        }
        return activateItem;
    }
}
