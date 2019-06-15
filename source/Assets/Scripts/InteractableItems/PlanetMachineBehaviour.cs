using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetMachineBehaviour : InteractableItemBehaviour
{
    [SerializeField] CharacterBehaviour character;
    [SerializeField] string itemName;
    [SerializeField] List<MeshRenderer> objects;
    [SerializeField] List<PlanetMachineButtonBehaviour> buttons;
    [SerializeField] List<Material> materials;
    [SerializeField] InventoryObjectBehaviour itemSpecial;
    [SerializeField] Material sunSpacialMaterial;


    protected override void ExecuteAction(CharacterBehaviour character)
    {
        if (character && character.CheckInventaryObjectOnSelectedPosition(itemName))
        {
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("PlaceSun", false, false);
            GameEvents.FSMEvents.StartInteraction.SafeInvoke(GameEnums.FSMInteractionEnum.ActivateItem);
            itemSpecial.gameObject.SetActive(true);
            itemSpecial.GetComponent<SphereCollider>().enabled = false;

            int index = 0;
            foreach (MeshRenderer renderer in objects)
            {
                PlanetMachineButtonBehaviour button = renderer.GetComponent<PlanetMachineButtonBehaviour>();
                if (button)
                {
                    button.isOn = true;
                }
                renderer.material = materials[index];
                index++;
            }
        }

    }

    private void Update()
    {
        //base.Shine();
    }

    public void CheckPlanets()
    {
        float position = -1;
        bool allPlanetsOnSameAngle = true;
        foreach (PlanetMachineButtonBehaviour planetButton in buttons)
        {
            if (position == -1)
            {
                position = planetButton.position;
            } else if (position != planetButton.position)
            {
                allPlanetsOnSameAngle = false;
                break; 
            }
        }

        if (allPlanetsOnSameAngle)
        {
            GameEvents.AudioEvents.TriggerSFX.SafeInvoke("EmpowerSun", false, false);
            itemSpecial.GetComponentInChildren<MeshRenderer>().material = sunSpacialMaterial;
            itemSpecial.GetComponent<SphereCollider>().enabled = true;
            int index = 0;
            for (index = 0; index < itemSpecial.transform.childCount; index++)
            {
                itemSpecial.transform.GetChild(index).gameObject.SetActive(true);
            }
        }
    }
}
