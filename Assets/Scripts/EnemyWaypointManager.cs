using UnityEngine;
using UnityEngine.UI;

public class EnemyWaypointManager : MonoBehaviour
{
  [SerializeField] private Camera mainCamera = null;
  [SerializeField] private EnemySpawner enemySpawner = null;
  [SerializeField] private Image waypointImage = null;
  /// <summary>
  /// Additional padding so the waypoint is not rendered right
  /// on top of the target.
  /// </summary>
  [SerializeField] private Vector3 waypointOffset = Vector3.zero;

  private EnemyController currentlyTrackedEnemy = null;
  private void Update()
  {
    // No more enemies to track. We'll disable the waypoint marker
    // and return.
    if (enemySpawner.AreAllEnemiesDead())
    {
      waypointImage.gameObject.SetActive(false);
      return;
    }

    // If we don't have an enemy to track, request one from the enemy spawner.
    if (currentlyTrackedEnemy == null || !currentlyTrackedEnemy.IsAlive)
      currentlyTrackedEnemy = enemySpawner.GetFirstAliveEnemy();

    // Transform enemy position from world space to screen space.
    Vector2 newWaypointPosition = mainCamera.WorldToScreenPoint(currentlyTrackedEnemy.transform.position + waypointOffset);

    newWaypointPosition = ClampWaypointToScreen(newWaypointPosition);

    waypointImage.transform.position = newWaypointPosition;
  }

  /// <summary>
  /// If the camera is turned away from the waypoint, we'd still like to see
  /// it in a reasonable location.
  /// </summary>
  /// <param name="waypointPosition">The position of the waypoint that needs to be modified.</param>
  /// <returns>The modified position that will be on the screen.</returns>
  private Vector2 ClampWaypointToScreen(Vector2 waypointPosition)
  {
    float minX = waypointImage.GetPixelAdjustedRect().width / 2f;
    float maxX = Screen.width - minX;

    float minY = waypointImage.GetPixelAdjustedRect().height / 2f;
    float maxY = Screen.height - minY;

    // This dot product will return -1 if the enemy we are tracking is
    // behind the camera's forward vector.
    if (Vector3.Dot((currentlyTrackedEnemy.transform.position - mainCamera.transform.position), mainCamera.transform.forward) < 0)
    {
      // The check succeeded so the enemy we're tracking is behind the camera.
      if (waypointPosition.x < Screen.width / 2f)
      {
        waypointPosition.x = maxX;
      }
      else
      {
        waypointPosition.x = minX;
      }
    }

    waypointPosition.x = Mathf.Clamp(waypointPosition.x, minX, maxX);
    waypointPosition.y = Mathf.Clamp(waypointPosition.y, minY, maxY);

    return waypointPosition;
  }
}
