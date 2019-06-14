using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Data", menuName = "LevelDefs", order = 1)]
public class LevelDefs : ScriptableObject
{
  [SerializeField] private LevelName levelName;
  [SerializeField] private string sceneName;
  [SerializeField] private Sprite loadingScreenBackground;
  [SerializeField] private float loadingDuration = 4f;

  public LevelName LevelName { get => levelName; }
  public string SceneName { get => sceneName; }
  public Sprite LoadingScreenBackground { get => loadingScreenBackground; }
  public float LoadingDuration { get => loadingDuration;}
}