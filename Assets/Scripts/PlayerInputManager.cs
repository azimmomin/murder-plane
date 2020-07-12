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

  public Vector2 GetChangeInPlayerInput()
  {
    return playerInputMoveAmount;
  }

  // Update is called once per frame
  private void Update()
  {
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
          initialTouch = touch;
          playerInputMoveAmount = Vector2.zero;
          break;
          // If the touch is a moving, rotate the player camera.
        case TouchPhase.Moved:
          playerInputMoveAmount = touch.position - initialTouch.position;
          break;
          // Clean up state once the touch has ended.
        case TouchPhase.Ended:
          initialTouch = EmptyTouch;
          playerInputMoveAmount = Vector2.zero;
          break;
      }
    }
  }
}
