﻿using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static Action OnGameReset;
  public static Action OnGameStarted;
  public static Action OnGameOver;

  [SerializeField] private EnemySpawner enemySpawner;
  [SerializeField] [Range(1, 10)] private int numEnemiesToSpawn = 3;

  private GameState gameState = GameState.Init;

  private void Awake()
  {
    DontDestroyOnLoad(gameObject);
  }

  private void Start()
  {
    ResetGame();
  }

  public void ResetGame()
  {
    enemySpawner.ClearSpawnedEnemies();
    enemySpawner.SpawnEnemies(numEnemiesToSpawn);
    OnGameReset?.Invoke();
    gameState = GameState.Ready;
  }

  public void StartGame()
  {
    gameState = GameState.Started;
    OnGameStarted?.Invoke();
  }

  private void Update()
  {
    switch (gameState)
    {
      case GameState.Started:
        if (enemySpawner.AreAllEnemiesDead())
        {
          gameState = GameState.Over;
          OnGameOver?.Invoke();
        }
        break;
      default:
        break;
    }
  }
}
