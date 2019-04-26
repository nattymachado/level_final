using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
   public string playerName;
   public List<LevelProgress> levels;

   public PlayerProgress(){
       playerName = "";
       levels = new List<LevelProgress>();
   }
}

[System.Serializable]
public class LevelProgress
{   
    public LevelName levelName;
    public bool levelConcluded;

    public LevelProgress(LevelName name, bool concluded){
        levelName = name;
        levelConcluded = concluded;
    }
}
