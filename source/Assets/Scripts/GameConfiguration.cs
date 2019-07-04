using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameConfiguration : Singleton<GameConfiguration>
{
  [SerializeField] private float _bgmVolume = 0f;
  [SerializeField] private float _sfxVolume = 0f;


    public void SetBGMVolume(float volume)
  {
        _bgmVolume = volume;
  }

  public void SetSFXVolume(float volume)
  {
        _sfxVolume = volume;
  }

  public float GetBGMVolume()
  {
        return _bgmVolume;
  }

  public float GetSFXVolume()
  {
    return _sfxVolume;
  }

}
