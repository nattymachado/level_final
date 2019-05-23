using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
  private static T m_Instance;
  public static T Instance { get { return m_Instance; } }


  private void Awake()
  {
    if (m_Instance != null && m_Instance != this)
    {
      DestroyImmediate(this.gameObject);
    }
    else
    {
      m_Instance = this as T;
      DontDestroyOnLoad(this.gameObject);
    }
  }
}