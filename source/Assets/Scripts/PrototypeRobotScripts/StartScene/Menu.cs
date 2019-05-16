using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartMenu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private string _nextScene;

        void Update()
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(_nextScene, LoadSceneMode.Single);
            }
        }
    }
}
