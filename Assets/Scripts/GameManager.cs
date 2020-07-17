using System;
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
    gameState = GameState.Ready;
    OnGameReset?.Invoke();
  }

  private void Update()
  {
    switch (gameState)
    {
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
