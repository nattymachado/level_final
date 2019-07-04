using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
  [SerializeField] private InputController inputController;
  CameraAnimation cameraAnimation;

  void Awake()
  {
    cameraAnimation = Camera.main.GetComponentInParent<CameraAnimation>();
  }

  void Start()
  {
    // inicia o level
    StartLevel();
  }

  private void StartLevel()
  {
    // desabilita interação do jogador
    PreventPlayerInteraction();

    // animate camera
    cameraAnimation.Approach(FinishCameraApproach);

    // triger do evento
    GameEvents.LevelEvents.LevelIntroStarted.SafeInvoke();
  }

  private void FinishCameraApproach(){
    // permite player interagir
    AllowPlayerInteraction();

    // trigger do evento
     GameEvents.LevelEvents.LevelStarted.SafeInvoke();
  }

  // pausa ou permite interação do jogador com input controller
  private void AllowPlayerInteraction()
  {
    inputController.SetActive(true);
  }

  private void PreventPlayerInteraction()
  {
    inputController.SetActive(false);
  }

}
