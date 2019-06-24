using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
   public string playerName;
   public bool completedTutorial = false;
   public List<LevelProgress> levels = new List<LevelProgress>();

   public PlayerProgress(string name){
       playerName = name;
       completedTutorial = false;
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
