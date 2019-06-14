using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static PlayerProgress currentProgress { get; private set; }
    private static string filePath = Application.persistentDataPath + "/gamesave.save";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Debug.Log("Default game save path: " + filePath);
        LoadProgressFile();
    }

    public static void LoadProgressFile()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            currentProgress = (PlayerProgress)bf.Deserialize(file);
            file.Close();
            Debug.Log("Progress Loaded!");
        }
        else
        {
            Debug.Log("Could not load progress. File does not exist.");
        }
    }

    public static void SaveProgressFile()
    {
        if (currentProgress != null)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(filePath);
            bf.Serialize(file, currentProgress);
            file.Close();
            Debug.Log("Progress Saved!");
        }
        else
        {
            Debug.Log("Progress file not initialized. Cannot save.");
        }
    }

    public static void CreateProgressFile(string playerName)
    {
        currentProgress = new PlayerProgress(playerName);
        Debug.Log("Create game progress for player: " + playerName);
        SaveProgressFile();
    }

    public static void DeleteProgressFile()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            currentProgress = null;
            Debug.Log("Progress file deleted.");
        }
        else
        {
            Debug.Log("Could not delete progress file. File does not exist.");
        }
    }

    public static LevelProgress GetLevelProgress(GameEnums.LevelName levelName){
        if (currentProgress == null)
        {
            Debug.Log("Progress file not initialized. Cannot add level progress.");
            return null;
        }

        for (int i = 0; i < currentProgress.levels.Count; i++)
        {
            if (currentProgress.levels[i].levelName == levelName)
            {
                return currentProgress.levels[i];
            }
        }

        LevelProgress lp = new LevelProgress(levelName);
        currentProgress.levels.Add(lp);
        return lp;
    }
}
