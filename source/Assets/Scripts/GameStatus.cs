using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStatus : Singleton<GameStatus>
{
    [SerializeField] private GameEnums.LevelEnum _lastLevel;
    [SerializeField] private List<GameEnums.PatientEnum> _deactivatePatients;


    public void SetLastLevel(GameEnums.LevelEnum level)
    {
        _lastLevel = level;
    }

    public GameEnums.LevelEnum GetLastLevel()
    {
        return _lastLevel;
    }

}
