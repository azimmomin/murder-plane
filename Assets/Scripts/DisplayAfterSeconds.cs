using System.Collections;
using UnityEngine;

/// <summary>
/// This class displays the specified gameobject after
/// the configured number of seconds. The countdown
/// starts when this game object becomes active.
/// </summary>
public class DisplayAfterSeconds : MonoBehaviour
{
  [SerializeField] private GameObject objectToDisplay = null;
  [SerializeField] private float secondsToWaitBeforeDisplay = 10f;

  private float timer = 0f;
  private bool isTimerActive = false;

  private void Start()
  {
    isTimerActive = true;
    StartCoroutine(DisplayObjectAfterSeconds());
  }

  private IEnumerator DisplayObjectAfterSeconds()
  {
    while (timer < secondsToWaitBeforeDisplay)
    {
      while (!isTimerActive)
        yield return null;

      timer += Time.deltaTime;
      yield return null;
    }

    objectToDisplay.SetActive(true);
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
    StopAllCoroutines();
  }
}
