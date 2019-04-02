using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaryObjectBehaviour2 : MonoBehaviour
{
    [SerializeField] public string Name;
    [SerializeField] public Image objectImage;
    [SerializeField] public InventaryCircleBehaviour inventaryCircle;



    void OnTriggerEnter(Collider other)
    {
        CharacterBehaviour2 character = other.GetComponent<CharacterBehaviour2>();
        if (character != null)
        {
            character.Inventary.Add(this);
            //IncludeItemOnInventary();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    private void IncludeItemOnInventary()
    {
        inventaryCircle.addNewItem(objectImage.sprite);
    }

    public void RemoveItemOnInventary()
    {
        //inventaryCircle.removeNewItem(objectImage.sprite);
        objectImage.gameObject.SetActive(false);
    }




}