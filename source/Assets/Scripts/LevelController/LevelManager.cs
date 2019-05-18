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
    cameraAnimation.Approach(AllowPlayerInteraction);
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
