using System;
using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// This class emulates an ad playing and toggles on
/// the play again button when it is done.
/// </summary>
public class AdTimer : MonoBehaviour
{
  [SerializeField] private float adTimeDurationInSeconds = 10f;
  [SerializeField] TextMeshProUGUI timerLabel;
  [SerializeField] private GameObject playAgainButton;

  private float timer = 0f;
  private bool isTimerActive = false;

  private void Awake()
  {
    GameManager.OnGameOver += StartAdTimer;
  }

  private void StartAdTimer()
  {
    timer = 0f;
    isTimerActive = true;
    StartCoroutine(DisplayObjectAfterSeconds());
  }

  private IEnumerator DisplayObjectAfterSeconds()
  {
    while (timer < adTimeDurationInSeconds)
    {
      while (!isTimerActive)
        yield return null;

      timer += Time.deltaTime;
      int timeLeft = (int)(adTimeDurationInSeconds - timer);
      timerLabel.text = timeLeft.ToString();
      yield return null;
    }

    playAgainButton.SetActive(true);
  }

  void OnApplicationFocus(bool hasFocus)
  {
    isTimerActive = !hasFocus;
  }

  private void OnApplicationPause(bool pauseStatus)
  {
    isTimerActive = pauseStatus;
  }

  private void OnDisable()
  {
    isTimerActive = false;
    playAgainButton.SetActive(false);
    StopAllCoroutines();
  }

  private void OnDestroy()
  {
    GameManager.OnGameOver -= StartAdTimer;
  }
}
