using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testesave : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerProgress progress;

    public void Initialize()
    {
        SaveManager.Initialize();
        progress = SaveManager.currentProgress;
    }

    public void Save()
    {
        SaveManager.SaveProgressFile();
        progress = SaveManager.currentProgress;
    }

    public void AddLevel()
    {
        SaveManager.AddLevelToProgress(LevelName.Cachorro, true);
        progress = SaveManager.currentProgress;
    }

    public void DeleteLevel()
    {
        SaveManager.DeleteProgressFile();
        progress = SaveManager.currentProgress;
    }
}
