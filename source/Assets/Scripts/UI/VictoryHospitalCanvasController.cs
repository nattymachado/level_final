using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryHospitalCanvasController : MonoBehaviour
{
    [SerializeField] GameObject overlayPanel;
    [SerializeField] GameObject fireworks;
    [SerializeField] Animator panelAnimator;
    [SerializeField] public bool Clicked;
    private const string CREDITS_SCENE = "credits";

    private void Awake()
    {
        fireworks.SetActive(false);
        overlayPanel.SetActive(false);
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

    public void Click()
    {
        SaveManager.DeleteProgressFile();
        SceneChanger.Instance.ChangeToScene(CREDITS_SCENE);
        Clicked = true;
    }
}
