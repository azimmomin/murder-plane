using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class is responsible for spawning enemies at random
/// spawn points.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private Transform[] enemySpawnPoints = null;
  [SerializeField] private GameObject enemyPrefab = null;

  public void SpawnEnemies(int amountToSpawn)
  {
    if (enemySpawnPoints == null || enemySpawnPoints.Length == 0)
    {
      Debug.LogError("Cannot spawn enemies because no spawn points configured");
      return;
    }

    // Bounds checks to make sure 0 <= amountToSpawn < enemySpawnPoints.Length
    int modifiedAmountToSpawn = Math.Max(0, Math.Min(amountToSpawn, enemySpawnPoints.Length));

    // Create an array of indicies that correspond to a spawn point, and shuffle that array
    System.Random rnd = new System.Random();
    var shuffledIndexRange = Enumerable.Range(0, enemySpawnPoints.Length).OrderBy(x => rnd.Next()).ToArray();

    // Now select the first N spawn points from the shuffled range, and spawn an
    // enemy prefab in that location.
    for (int i = 0; i < modifiedAmountToSpawn; i++)
    {
      Transform spawnPoint = enemySpawnPoints[shuffledIndexRange[i]];
      Instantiate(enemyPrefab, spawnPoint);
    }
  }
}
