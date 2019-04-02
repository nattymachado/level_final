using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPilarBehaviour : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] private float Angle;
    [SerializeField] GameObject sun;
    [SerializeField] Transform sunPosition;
    [SerializeField] Transform otherPlanet;
    [SerializeField] Material newSunMaterial;
    public bool isEnded = false;

    public IEnumerator HideSun(float interval, CharacterBehaviour2 character)
    {
        yield return new WaitForSeconds(interval);
        sun.SetActive(false);
        character.Inventary.Add(sun.GetComponent<InventaryObjectBehaviour2>());
        character.SelectedItemPosition = 1;
    }

    public void RotateCameraToRight(CharacterBehaviour2 character)
    {
        if (target  && !isEnded)
        {
            //transform.RotateAround(transform.position,target.position, Angle);
            transform.RotateAround(target.position, new Vector3(0.0f, -1.0f, 0.0f), Angle);
            if (((System.Math.Round(otherPlanet.position.x, 1) == System.Math.Round(transform.position.x, 1)) ||
                (System.Math.Round(otherPlanet.position.z, 1) == System.Math.Round(transform.position.z, 1))) &&
                 ((System.Math.Round(sunPosition.position.x, 1) == System.Math.Round(transform.position.x, 1)) ||
                (System.Math.Round(sunPosition.position.z, 1) == System.Math.Round(transform.position.z, 1)))){
                sun.GetComponent<MeshRenderer>().material = newSunMaterial;
                StartCoroutine(HideSun(10, character));
                

                otherPlanet.GetComponent<PlanetPilarBehaviour>().isEnded = true;
                isEnded = true;
            }
        }
    }

}
