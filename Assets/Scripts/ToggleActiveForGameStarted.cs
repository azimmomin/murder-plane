using UnityEngine;

/// <summary>
/// This class is responsible for toggling a gameobject
/// active or inactive when the GameStarted event is fired.
/// </summary>
public class ToggleActiveForGameStarted : MonoBehaviour
{
  [SerializeField] private bool isActive = false;

  private void Awake()
  {
    GameManager.OnGameStarted += Toggle;
  }

  private void Toggle()
  {
    gameObject.SetActive(isActive);
  }

  private void OnDestroy()
  {
    GameManager.OnGameStarted -= Toggle;
  }
}
