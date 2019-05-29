using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStatus : Singleton<GameStatus>
{
    [SerializeField] private GameEnums.LevelEnum _lastLevel;
    [SerializeField] private List<GameEnums.PatientEnum> _deactivatedPatients;


    public void SetLastLevel(GameEnums.LevelEnum level)
    {
        _lastLevel = level;
    }

    public GameEnums.LevelEnum GetLastLevel()
    {
        return _lastLevel;
    }

    public void IncludeDeactivatePatient(GameEnums.PatientEnum deactivatedPatient)
    {
        _deactivatedPatients.Add(deactivatedPatient);
    }

    public bool CheckIfPatientIsDeactivated(GameEnums.PatientEnum deactivatedPatient)
    {
        Debug.Log("Checking...");
        return _deactivatedPatients.Contains(deactivatedPatient);
    }

}
