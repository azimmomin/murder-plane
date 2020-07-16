using UnityEngine;

/// <summary>
/// This class is responsible for toggling a gameobject
/// active or inactive when the GameReset event is fired.
/// </summary>
public class ToggleActiveForGameReset : MonoBehaviour
{
  [SerializeField] private bool isActive = false;

  private void Awake()
  {
    GameManager.OnGameReset += Toggle;
  }

  private void Toggle()
  {
    gameObject.SetActive(isActive);
  }

  private void OnDestroy()
  {
    GameManager.OnGameReset -= Toggle;
  }
}
