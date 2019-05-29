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
    panelAnimator.SetBool("opened", false);
  }

  private void Update(){
    // teste
  }

  public void Open()
  {
    // liga painel
    overlayPanel.SetActive(true);

    // executa animação
    panelAnimator.SetBool("opened", true);
  }

  public void Continue()
  {
    SceneChanger.Instance.ChangeToScene("hospital");
  }
}
