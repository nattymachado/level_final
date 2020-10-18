using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpotlightBehaviour : InteractableItemBehaviour
{

    [SerializeField] string lightName;
    [SerializeField] List<LightGateController.LightIdsEnum> lightIds;
    [SerializeField] List<GameObject> lightObjects;
    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;
    [SerializeField] LightGateController lightController;
    [SerializeField] GameObject light;
    [SerializeField] bool isOn = false;
    Animator _animator;
    const string positionName = "position";
    int _position = 0;
    int _old_position = 0;



    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator != null)
        {
            _animator.SetInteger(positionName, _position + 1);
        }
        
    }

    protected override void ExecuteAction(CharacterBehaviour character)
    {
        
        if (!isOn)
        {
            if (character && character.CheckInventaryObjectOnSelectedPosition(lightName))
            {
                GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
                isOn = true;
                _isActive = false;
                light.SetActive(true);
                lightController.AddActiveLight(lightIds[_position]);
                lightObjects[_position].GetComponent<Renderer>().material = OnMaterial;
            }
        } else
        {
            
            _isActive = false;
            if (lightIds.Count > 1)
            {
                GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            }
            _old_position = _position;
            if ( (_position + 1) == lightIds.Count)
            {
                _position = 0;
            } else
            {
                _position += 1;
            }
            if (_animator)
            {
                _animator.SetInteger(positionName, _position + 1);
            }

            lightController.ChangeLight(lightIds[_position], lightIds[_old_position]);
            StartCoroutine(ChangeLight());
        }

    }

    public IEnumerator ChangeLight()
    {
        yield return new WaitForSeconds(0.5f);
        if (lightObjects[_position] != null)
        {
            lightObjects[_position].GetComponent<Renderer>().material = OnMaterial;
        }
        if (lightObjects[_old_position] != null)
        {
            lightObjects[_old_position].GetComponent<Renderer>().material = OffMaterial;
        }
        
    }







}
