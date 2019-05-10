using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MovimentController : MonoBehaviour
{
  private LayerMask _raycastMaskFloor;
  private LayerMask _raycastMaskItem;
  [SerializeField] private CharacterBehaviour _character;
  [SerializeField] private Pointer pointer;

  private void Awake()
  {
    _raycastMaskFloor = LayerMask.GetMask(new string[] { "Floor" });
    _raycastMaskItem = LayerMask.GetMask(new string[] { "Interactable" });

  }

  public void Move(Vector3 position)
  {
    Ray ray = Camera.main.ScreenPointToRay(position);

    RaycastHit[] hits = new RaycastHit[1];
    Physics.RaycastNonAlloc(ray, hits, 500f, _raycastMaskFloor);

    if (hits[0].collider != null)
    {
      pointer.gameObject.SetActive(true);
      pointer.transform.position = hits[0].point;
      _character.Move(hits[0].point);
    }
  }

  public bool ActiveItem(Vector3 position)
  {
    bool activateItem = false;
    Ray ray = Camera.main.ScreenPointToRay(position);

    RaycastHit[] hits = new RaycastHit[1];
    Physics.RaycastNonAlloc(ray, hits, 100f, _raycastMaskItem);

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
