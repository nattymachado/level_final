using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "Data", menuName = "LevelDefs", order = 1)]
public class LevelDefs : ScriptableObject
{
  [SerializeField] private LevelName levelName;
  [SerializeField] private string sceneName;
  [SerializeField] private VideoClip loadingVideo;
  [SerializeField] private float loadingDuration = 4f;

  public LevelName LevelName { get => levelName; }
  public string SceneName { get => sceneName; }
  public VideoClip LoadingVideo { get => loadingVideo; }
  public float LoadingDuration { get => loadingDuration;}
}