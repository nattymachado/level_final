using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameConfiguration : Singleton<GameConfiguration>
{
  [SerializeField] private float bgmVolume = 0f;
  [SerializeField] private float sfxVolume = 0f;
  

  public void SetBGMVolume(float volume)
  {
        Debug.Log(volume);
        bgmVolume = volume;
  }

  public void SetSFXVolume(float volume)
  {
        Debug.Log(volume);
        sfxVolume = volume;
  }

  public float GetBGMVolume()
  {
        return bgmVolume;
  }

  public float GetSFXVolume()
  {
    return sfxVolume;
  }

}
