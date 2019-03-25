using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BillboardText : MonoBehaviour
{
  void Update(){

      transform.LookAt(Camera.main.transform.position);
  }
}
