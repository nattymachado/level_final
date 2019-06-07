using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleInventoryController : Singleton<CollectibleInventoryController>
{
    //Refence Variables
    [Header("Required References")]
    public GameObject[] specialSlots;

    //Control Variables
    [Header("Control Variables")]
    public int totalSpecialSlots = 3;
    private int currentSlot = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Disable Extra Slots
        for(int i = totalSpecialSlots; i < 3; i++)
        {
            specialSlots[i].SetActive(false);
        }
    }

    public Vector2 GetNextVacantSlotScreenPosition()
    {
        Debug.Log(specialSlots[currentSlot].transform.position);
        return specialSlots[currentSlot].transform.position;
    }

    public void AddItem(Sprite imageSprite)
    {
        Image imageRef = specialSlots[currentSlot].transform.GetChild(0).GetComponent<Image>();
        imageRef.sprite = imageSprite;
        imageRef.enabled = true;
        currentSlot++;
    }
}
