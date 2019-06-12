using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class InventoryObjectBehaviour : MonoBehaviour
{
    public GameEnums.ItemTypeEnum itemType;
    public Transform objectCenter;
    [SerializeField] public string Name;
    [SerializeField] public Image objectImage;
    [SerializeField] public InventoryCenterBehaviour inventaryCenter;
    private Animator _animator;
    private bool _isEnabled = true;
    private Vector3 _targetMovementLocation = Vector3.zero;
    private float animationMovementSpeed;
    private float animationTime = 1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isEnabled)
        {
            CharacterBehaviour character = other.GetComponent<CharacterBehaviour>();
            if (character != null)
            {
                _isEnabled = false;
                GetItem(character);
            }
        }
    }

    private void GetItem(CharacterBehaviour character)
    {
        character.SetRotation(transform);
        StartCoroutine(WaitToIncludeOnInventory(0f, character));
    }

    IEnumerator WaitToIncludeOnInventory(float seconds, CharacterBehaviour character)
    {
        yield return new WaitForSeconds(seconds);
        GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.PickupItem);
        IncludeItemOnInventory(character);
    }

    private void IncludeItemOnInventory(CharacterBehaviour character)
    {
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("ItemPickup", false, false);

        if (_animator != null) _animator.SetBool("IsGoingToInventary", true);
    }

    public void DisableItem()
    {
        //Add to Inventory
        if (itemType == GameEnums.ItemTypeEnum.Generic) inventaryCenter.AddNewItem(this);
        else if (itemType == GameEnums.ItemTypeEnum.Collectible) CollectibleInventoryController.Instance.AddItem(objectImage.sprite);

        //Finally...
        GameEvents.FSMEvents.FinishedInteraction.SafeInvoke(); //Unlock Inputs
        gameObject.SetActive(false);
    }

    public void TriggerMovementAnimation()
    {
        _targetMovementLocation = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth - 150, Camera.main.pixelHeight - 150, 5f));
        animationMovementSpeed = Vector3.Distance(objectCenter.position, _targetMovementLocation) / animationTime;
    }

    public void TriggerCollectibleItemMovementAnimation()
    {
        Vector2 screenPosition = CollectibleInventoryController.Instance.GetNextVacantSlotScreenPosition();
        Debug.Log("Target:" + _targetMovementLocation);
        _targetMovementLocation = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Vector3.Distance(Camera.main.transform.position, this.transform.position)));
        animationMovementSpeed = Vector3.Distance(objectCenter.position, _targetMovementLocation) / animationTime;
    }

    private void Update()
    {
        if(_targetMovementLocation != Vector3.zero)
        {
            if (this.transform.position == _targetMovementLocation)
            {
                _targetMovementLocation = Vector3.zero;
                DisableItem();
            }
            else
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, _targetMovementLocation, animationMovementSpeed * Time.deltaTime);
            }
        }
    }
}
