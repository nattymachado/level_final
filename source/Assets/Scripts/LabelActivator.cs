using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelActivator : MonoBehaviour
{
    [SerializeField] private Transform labelParent;
    [SerializeField] private Text label;
    [SerializeField] private string text;

    private Collider area;
    private Transform player;

    private void Awake(){
        area = GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        label.text = text;
    }

    private void Update(){

        // check if player is inside area to activate label
        if(area.bounds.Contains(player.position)){
            labelParent.gameObject.SetActive(true);
        } else {
            labelParent.gameObject.SetActive(false);
        }

    }
}
