using UnityEngine;

/// <summary>
/// This class is responsible for toggling gameobjects
/// active or inactive when the GameStarted event is fired.
/// </summary>
public class ToggleActiveForGameStarted : MonoBehaviour
{
  [SerializeField] private bool isActive = false;
  [SerializeField] private GameObject[] objectsToToggle;

  private void Awake()
  {
    GameManager.OnGameStarted += Toggle;
  }

  private void Toggle()
  {
    foreach (GameObject obj in objectsToToggle)
    {
      obj.SetActive(isActive);

    }
  }
  private void OnDestroy()
  {
    GameManager.OnGameStarted -= Toggle;
  }
}
