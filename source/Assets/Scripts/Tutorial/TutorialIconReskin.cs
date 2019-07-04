using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialIconReskin : MonoBehaviour
{

  private enum Platform { PC, Mobile };
  private Platform currentPlatform = Platform.Mobile;
  public List<SpriteRenderer> renderers;
  public List<Image> images;
  public List<IconPair> iconPairs = new List<IconPair>();
  string currentSpriteName, currentImageName;

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
            renderer.sprite = pair.pcSprite;
            break;
          }
        }
      }
      foreach (var image in images)
      {
        currentImageName = image.sprite.name;
        foreach (var pair in iconPairs)
        {
          if (pair.mobileSprite.name == currentImageName)
          {
            image.sprite = pair.pcSprite;
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