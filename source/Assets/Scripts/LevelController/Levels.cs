using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelName {NULL,Robot,Night}

[CreateAssetMenu(fileName = "Levels", menuName = "Levels", order = 1)]
public class Levels : ScriptableObject
{
    public List<LevelDefs> levels;
}
