using UnityEngine;

public class ToggleActiveForGameOver : MonoBehaviour
{
  [SerializeField] private bool isActive = false;

  private void Awake()
  {
    GameManager.OnGameOver += Toggle;
  }

  private void Toggle()
  {
    gameObject.SetActive(isActive);
  }

  private void OnDestroy()
  {
    GameManager.OnGameOver -= Toggle;
  }
}
