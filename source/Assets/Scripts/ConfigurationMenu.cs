using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ConfigurationMenu : MonoBehaviour
{
    [SerializeField] private GameObject _creditsPanel;
    [SerializeField] private GameObject _previousPanel;
    [SerializeField] private GameObject _configurationPanel;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _creditButton;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private bool _showCredit = false;

    public void Configuration()
    {
        Debug.Log("Estou aqui");
        if (Application.platform == RuntimePlatform.Android)
            _quitButton.gameObject.SetActive(false);
        else
            _quitButton.gameObject.SetActive(true);

        _creditButton.gameObject.SetActive(_showCredit);
       
        if (GameConfiguration.Instance != null)
        {
            _bgmSlider.value = GameConfiguration.Instance.GetBGMVolume();
            _sfxSlider.value = GameConfiguration.Instance.GetSFXVolume();
        }

        this.gameObject.SetActive(true);
        _configurationPanel.SetActive(true);
        if(_previousPanel != null) _previousPanel.SetActive(false);
        _creditsPanel.SetActive(false);
    }

    public void Credits()
    {
        _configurationPanel.SetActive(false);
        _creditsPanel.SetActive(true);
    }

    public void CloseConfiguration()
    {
        this.gameObject.SetActive(false);
        _configurationPanel.SetActive(false);
        if (_previousPanel != null) _previousPanel.SetActive(true);
        _creditsPanel.SetActive(false);
    }

    public void CloseCredits()
    {
        _configurationPanel.SetActive(true);
        _creditsPanel.SetActive(false);
    }

    public void UpdateSFXVolume()
    {
        GameConfiguration.Instance.SetSFXVolume(_sfxSlider.value);
        GameEvents.AudioEvents.SetSFXVolume.SafeInvoke(_sfxSlider.value);
    }

    public void UpdateBGMVolume()
    {
        GameConfiguration.Instance.SetBGMVolume(_bgmSlider.value);
        GameEvents.AudioEvents.SetBGMVolume.SafeInvoke(_bgmSlider.value);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

