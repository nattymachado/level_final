using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    private static PlayerProgress currentProgress;
    private static string filePath = Application.persistentDataPath + "/gamesave.save";

    public static PlayerProgress LoadProgressFile(){
        PlayerProgress save = null;
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            save = (PlayerProgress)bf.Deserialize(file);
            file.Close();
            Debug.Log("Progress Loaded");
        }
        return save;
    }

    public static void SaveProgressFile(PlayerProgress save){
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Progress Saved");
    }

    public static void AddLevel(LevelName levelName, bool concluded){
        currentProgress.levels.Add(new LevelProgress(levelName,concluded));
    }
}
