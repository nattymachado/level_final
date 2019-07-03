using UnityEngine;
using System;
using System.Collections.Generic;

public class TutorialIconReskin : MonoBehaviour
{

  private enum Platform { PC, Mobile };
  private Platform currentPlatform = Platform.Mobile;
  public SpriteRenderer renderers;
  public List<IconPair> iconPairs = new List<IconPair>();
  string currentSpriteName;

  void Awake()
  {
    if (Application.platform == RuntimePlatform.WindowsPlayer ||
    Application.platform == RuntimePlatform.LinuxPlayer ||
    Application.platform == RuntimePlatform.OSXPlayer)
    {
      currentPlatform = Platform.PC;
    }
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.P))
    {
      TogglePlatform();
    }
  }

  void LateUpdate()
  {
    if (currentPlatform == Platform.PC)
    {
      foreach (var renderer in renderers)
      {
        currentSpriteName = renderer.sprite.name;
        foreach (var pair in iconPairs)
        {
          if (pair.mobileSprite.name == currentSpriteName)
          {
            renderer.sprite = iconPairs.pcSprite;
            break;
          }
        }
      }
    }
  }

  void TogglePlatform()
  {
    if (currentPlatform == Platform.Mobile) currentPlatform = Platform.PC;
    else if (currentPlatform == Platform.PC) currentPlatform = Platform.Mobile;
  }

}

[Serializable]
public class IconPair
{
  public Sprite mobileSprite;
  public Sprite pcSprite;
}