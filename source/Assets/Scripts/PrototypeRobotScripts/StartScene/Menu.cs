using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private string _nextScene;

        public void Play()
        {
            SceneManager.LoadScene(_nextScene, LoadSceneMode.Single);
        }
    }
}
