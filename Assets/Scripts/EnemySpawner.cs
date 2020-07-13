using System;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class is responsible for spawning enemies at random
/// spawn points.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private Transform[] enemySpawnPoints;
  [SerializeField] private GameObject enemyPrefab;

  public void SpawnEnemies(int amountToSpawn)
  {
    if (enemySpawnPoints.Length == 0)
    {
      Debug.LogError("Cannot spawn enemies because no spawn points configured");
      return;
    }

    // Bounds checks to make sure 0 <= amountToSpawn < enemySpawnPoints.Length
    int modifiedAmountToSpawn = Math.Max(0, Math.Min(amountToSpawn, enemySpawnPoints.Length));
    System.Random rnd = new System.Random();
    var shuffledIndexRange = Enumerable.Range(0, enemySpawnPoints.Length).OrderBy(x => rnd.Next()).ToArray();

    for (int i = 0; i < modifiedAmountToSpawn; i++)
    {
      Transform spawnPoint = enemySpawnPoints[shuffledIndexRange[i]];
      Instantiate(enemyPrefab, spawnPoint);
    }
  }
}
