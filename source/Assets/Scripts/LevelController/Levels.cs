using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Levels", menuName = "Levels", order = 1)]
public class Levels : ScriptableObject
{
    public List<LevelDefs> levels;
}
