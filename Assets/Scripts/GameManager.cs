using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static Action OnGameInit;
  public static Action OnGameReady;
  public static Action OnGameStarted;
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
        OnGameReady?.Invoke();
        break;
      case GameState.Ready:
        // TODO Remove test code.
#if UNITY_EDITOR
        if (Input.anyKey)
        {
          gameState = GameState.Started;
          OnGameStarted?.Invoke();
        }
#else
        if (Input.touchCount > 0)
        {
          Touch touch = Input.GetTouch(0);
          if (touch.fingerId == 0 && touch.phase == TouchPhase.Ended)
          {
            gameState = GameState.Started;
            OnGameStarted?.Invoke();
          }
        }
#endif
        break;
      case GameState.Started:
        // TODO Play an animation to not make this so abrupt.
        if (enemySpawner.AreAllEnemiesDead())
        {
          gameState = GameState.Over;
          OnGameOver?.Invoke();
        }
        break;
      case GameState.Over:
        // TODO: Display Game Over UI.
        break;
      default:
        break;
    }
  }
}
