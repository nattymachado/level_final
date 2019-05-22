using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StartMenu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private string _nextScene;
        [SerializeField] private GameObject _configurationPanel;
        [SerializeField] private GameObject _principalPanel;

        public void Play()
        {
            SceneManager.LoadScene(_nextScene, LoadSceneMode.Single);
        }

        public void Configuration()
        {
            _configurationPanel.SetActive(true);
            _principalPanel.SetActive(false);
        }

        public void Close()
        {
            _configurationPanel.SetActive(false);
            _principalPanel.SetActive(true);
        }
    }
}
