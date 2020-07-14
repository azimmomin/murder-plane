using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static Action OnGameInit;
  public static Action OnGameOver;

  public static GameManager Instance
  {
    get
    {
      if (instance == null)
      {
        GameManager[] instances = FindObjectsOfType<GameManager>();
        if (instances.Length != 1)
        {
          throw new System.Exception(
              $"Invalid number of GameManager instances in the current scene. Count={instances.Length}");
        }

        instance = instances[0];
      }

      return instance;
    }
  }

  private static GameManager instance = null;
  static GameManager() { }


  public bool IsGameActive { get; private set; }

  [SerializeField] private EnemySpawner enemySpawner;
  [SerializeField] [Range(1, 10)] private int numEnemiesToSpawn = 3;

  private GameState gameState = GameState.Init;

  private GameManager() { }

  private void Awake()
  {
    DontDestroyOnLoad(this);
  }

  private void Update()
  {
    switch (gameState)
    {
      case GameState.Init:
        enemySpawner.ClearSpawnedEnemies();
        enemySpawner.SpawnEnemies(numEnemiesToSpawn);
        OnGameInit?.Invoke();
        gameState = GameState.Ready;
        break;
      case GameState.Ready:
        if (Input.touchCount > 0)
        {
          Touch touch = Input.GetTouch(0);
          if (touch.fingerId == 0 && touch.phase == TouchPhase.Ended)
          {
            gameState = GameState.Started;
          }
        }
        break;
      case GameState.Started:
        IsGameActive = true;
        // TODO Play an animation to not make this so abrupt.
        if (enemySpawner.AreAllEnemiesDead())
          gameState = GameState.Over;
        break;
      case GameState.Over:
        IsGameActive = false;
        OnGameOver?.Invoke();
        // TODO: Display Game Over UI.
        break;
      default:
        break;
    }
  }
}
