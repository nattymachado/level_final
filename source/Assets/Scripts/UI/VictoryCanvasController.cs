using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryCanvasController : MonoBehaviour
{
    [SerializeField] GameObject overlayPanel;
    [SerializeField] Animator panelAnimator;
    [SerializeField] GameObject fireworks;

    private void Awake()
    {
        overlayPanel.SetActive(false);
        if (fireworks != null)
        {
            fireworks.SetActive(false);
        }
        
    }

    public void Open()
    {
        // liga painel
        overlayPanel.SetActive(true);
        fireworks.SetActive(true);

        // executa animação
        panelAnimator.SetBool("opened", true);
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Fanfare", false, true);
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Fireworks", true, true);
    }

    public void Continue()
    {
        GameEvents.AudioEvents.TriggerSFX.SafeInvoke("Click_Light", false, true);
        SceneChanger.Instance.ChangeToScene("hospital");
    }
}
