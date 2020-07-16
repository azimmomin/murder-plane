using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
  // Creating an empty touch object so we don't
  // have to keep initializing new ones.
  private static Touch EmptyTouch = new Touch();

  // This tracks a touch when it begins so we can use it
  // to calculate the swipe direction vectore if the player
  // swipes the touch.
  private Touch initialTouch = EmptyTouch;
  private Vector2 playerInputMoveAmount = Vector2.zero;
  private bool isActive = false;

  public Vector2 GetChangeInPlayerInput()
  {
    return playerInputMoveAmount;
  }

  private void Awake()
  {
    GameManager.OnGameStarted += SetInputActive;
    GameManager.OnGameOver += SetInputInactive;
  }

  private void SetInputActive()
  {
    isActive = true;
  }

  private void SetInputInactive()
  {
    isActive = false;
  }

  // Update is called once per frame
  private void Update()
  {
    if (!isActive) return;

    // Rotate the plane based on user input
    HandlePlayerInput();
  }

  private void HandlePlayerInput()
  {
    foreach (Touch touch in Input.touches)
    {
      // For the sake of simplicity, we'll only concern ourselves with one touch.
      if (touch.fingerId != 0)
        continue;

      switch (touch.phase)
      {
        case TouchPhase.Began:
        case TouchPhase.Stationary:
          initialTouch = touch;
          playerInputMoveAmount = Vector2.zero;
          break;
        // If the touch is a moving, capture the amount it moved.
        case TouchPhase.Moved:
          playerInputMoveAmount = touch.position - initialTouch.position;
          break;
        // Clean up state once the touch has ended.
        case TouchPhase.Canceled:
        case TouchPhase.Ended:
          initialTouch = EmptyTouch;
          playerInputMoveAmount = Vector2.zero;
          break;
      }
    }
  }

  private void OnDestroy()
  {
    GameManager.OnGameStarted -= SetInputActive;
    GameManager.OnGameOver -= SetInputInactive;
  }
}
