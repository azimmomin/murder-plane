using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class is responsible for spawning enemies at random
/// spawn points and keeping track of those spawned enemies.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
  [SerializeField] private Transform[] enemySpawnPoints = null;
  [SerializeField] private GameObject enemyPrefab = null;

  private IList<EnemyController> spawnedEnemies = new List<EnemyController>();

  /// <summary>
  /// Attempts to spawn the requested amount in random
  /// spawn points specified in the serialized field above.
  /// </summary>
  /// <param name="amountToSpawn"></param>
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
      GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint);
      EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
      if (enemyController != null)
        spawnedEnemies.Add(enemyController);
    }
  }

  public bool AreAllEnemiesDead()
  {
    bool areAllEnemiesDead = true;
    for (int i = 0, count = spawnedEnemies.Count; i < count; i++)
      areAllEnemiesDead &= spawnedEnemies[i].IsDead;

    return areAllEnemiesDead;
  }

  public void ClearSpawnedEnemies()
  {
    for (int i = 0, count = spawnedEnemies.Count; i < count; i++)
      Destroy(spawnedEnemies[i].gameObject);

    spawnedEnemies.Clear();
  }

  /// <returns>EnemyController if there is an alive enemy left, null otherwise.</returns>
  public EnemyController GetFirstAliveEnemy()
  {
    foreach (EnemyController enemy in spawnedEnemies)
    {
      if (enemy.IsAlive)
        return enemy;
    }

    return null;
  }
}
