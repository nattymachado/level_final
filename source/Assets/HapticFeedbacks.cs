using UnityEngine;

public class HapticFeedbacks : MonoBehaviour
{
  void OnEnable()
  {
    GameEvents.GameStateEvents.GameStarted += Vibrate;
    GameEvents.GameStateEvents.LevelEntered += Vibrate;
    GameEvents.GameStateEvents.LevelCompleted += Vibrate;
    GameEvents.Interactables.DreamMachine += Vibrate;
  }

  void OnDisable()
  {
    GameEvents.GameStateEvents.GameStarted -= Vibrate;
    GameEvents.GameStateEvents.LevelEntered -= Vibrate;
    GameEvents.GameStateEvents.LevelCompleted -= Vibrate;
    GameEvents.Interactables.DreamMachine -= Vibrate;
  }

  private void Vibrate()
  {
    Handheld.Vibrate();
  }
}
