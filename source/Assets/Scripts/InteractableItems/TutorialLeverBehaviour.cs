using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLeverBehaviour : InteractableItemBehaviour
{
  [SerializeField] ParticleSystem particles;

  protected override void ExecuteAction(Collider other)
  {
    SetActive(false);
  }

  public void TurnParticlesOn()
  {
    particles.Play();
  }

  public void TurnParticlesOff()
  {
    particles.Stop();
  }
}
