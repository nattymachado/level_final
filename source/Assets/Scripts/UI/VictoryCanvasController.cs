using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCanvasController : MonoBehaviour
{
    [SerializeField] GameObject overlayPanel;
    [SerializeField] Animator panelAnimator;

  private void Awake()
  {
    overlayPanel.SetActive(false);
  }

    public void Open()
    {
        // liga painel
        overlayPanel.SetActive(true);

        // executa animação
        panelAnimator.SetBool("opened", true);
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Fanfare", false, true);
    }

    public void Continue()
    {
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Click_Light", false, false);
        SceneChanger.Instance.ChangeToScene("hospital");
    }
}
