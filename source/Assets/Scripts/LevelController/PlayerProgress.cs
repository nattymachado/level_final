using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
   public string playerName;
   public List<LevelProgress> levels;

   public PlayerProgress(string name){
       playerName = name;
       levels = new List<LevelProgress>();
   }
}

[System.Serializable]
public class LevelProgress
{   
    public GameEnums.LevelName levelName;
    public bool levelConcluded;
    public bool patientLeftBed;

    public LevelProgress(GameEnums.LevelName name){
        levelName = name;
        levelConcluded = false;
        patientLeftBed = false;
    }
}
