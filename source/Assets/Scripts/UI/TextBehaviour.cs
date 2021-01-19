using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class TextBehaviour : MonoBehaviour
{

 public TMP_Text textMesh;
 
 private void Start() {

    textMesh = GetComponent<TMP_Text>();//
    textMesh.text = LocatedTexts.Instance.GetKey("settings");
 }
        
}
