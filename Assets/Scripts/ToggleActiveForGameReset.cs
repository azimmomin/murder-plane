using UnityEngine;

/// <summary>
/// This class is responsible for toggling gameobjects
/// active or inactive when the GameReset event is fired.
/// </summary>
public class ToggleActiveForGameReset : MonoBehaviour
{
  [SerializeField] private bool isActive = false;
  [SerializeField] private GameObject[] objectsToToggle;

  private void Awake()
  {
    GameManager.OnGameReset += Toggle;
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
    GameManager.OnGameReset -= Toggle;
  }
}
