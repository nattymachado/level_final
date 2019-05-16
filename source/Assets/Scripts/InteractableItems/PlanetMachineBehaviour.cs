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


    protected override void ExecuteAction(Collider other)
    {
        if (character && character.CheckInventaryObjectOnSelectedPosition(itemName))
        {
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
            itemSpecial.GetComponent<MeshRenderer>().material = sunSpacialMaterial;
            itemSpecial.GetComponent<SphereCollider>().enabled = true;
        }


    }


}
