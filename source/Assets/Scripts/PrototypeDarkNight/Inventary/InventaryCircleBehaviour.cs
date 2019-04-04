using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaryCircleBehaviour : MonoBehaviour
{
    [SerializeField] float rotSpeed = 6;
    [SerializeField] List<Image> inventaryPositions = new List<Image>();
    [SerializeField] CharacterBehaviour2 character;
    [SerializeField] Sprite emptySprite;
    float positionCircle = 0;
    bool lastIsRight = false;
    private int nextItemPosition = 0;



    public void RotateItem(bool isRight)
    {
        Vector3 rotateVector;
        if (isRight)
        {
            rotateVector = new Vector3(0.0f, 0.0f, 45f) * rotSpeed * Time.deltaTime;
            transform.Rotate(rotateVector);

        }
        else
        {
            rotateVector = new Vector3(0.0f, 0.0f, -45f) * rotSpeed * Time.deltaTime;

            transform.Rotate(new Vector3(0.0f, 0.0f, -45f) * rotSpeed * Time.deltaTime);
        }
        lastIsRight = isRight;
        positionCircle += rotateVector.z;
        positionCircle = positionCircle < 0 ? 360 + positionCircle : positionCircle;
        positionCircle = positionCircle > 360 ? positionCircle - 360 : positionCircle;
        Debug.Log(positionCircle);

    }





    public void addNewItem(InventaryObjectBehaviour2 item)
    {
        inventaryPositions[nextItemPosition].sprite = item.objectImage.GetComponent<Image>().sprite;
        item.Position = nextItemPosition;
        nextItemPosition += 1;

    }

    public void removeNewItem(int position)
    {
        inventaryPositions[position].sprite = emptySprite;

    }

    public void FixRotation()
    {
        if (lastIsRight)
        {
            if (positionCircle < 90)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 90f);
                character.SelectedItemPosition = 1;
            }
            else if (positionCircle < 180)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 180f);
                character.SelectedItemPosition = 2;
            }
            else if (positionCircle < 270)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 270f);
                character.SelectedItemPosition = 3;
            }
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0f);
                character.SelectedItemPosition = 0;
            }
        }
        else
        {
            if (positionCircle < 90)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0f);
                character.SelectedItemPosition = 0;
            }
            else if (positionCircle < 180)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 90f);
                character.SelectedItemPosition = 1;
            }
            else if (positionCircle < 270)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 180f);
                character.SelectedItemPosition = 2;
            }
            else
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 270f);
                character.SelectedItemPosition = 3;
            }

        }
        positionCircle = transform.eulerAngles.z;
    }
}