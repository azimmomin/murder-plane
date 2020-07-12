using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class moves the player based on user input. 
/// </summary>
public class PlayerController : MonoBehaviour
{
  // TODO: Refactor Touch logic into its own class.
  // Creating an empty touch object so we don't
  // have to keep initializing new ones.
  private static Touch EmptyTouch = new Touch();

  [SerializeField] private float speed = 10f;
  [SerializeField] private float rotationSpeed = 10f;
  [SerializeField] private Camera playerCamera = null;
  [SerializeField] private float minRotationAngle = -45f;
  [SerializeField] private float maxRotationAngle = 45f;

  private Touch activeTouch = EmptyTouch;

  void Update()
  {
    // Move the plane forward at a constant speed
    transform.Translate(0f, 0f, speed * Time.deltaTime);

    HandlePlayerInput();
    //float xRotation = verticalInput * rotationSpeed * Time.deltaTime;
    //float yRotation = horizontalInput * rotationSpeed * Time.deltaTime;
    //transform.Rotate(xRotation, yRotation, 0f, Space.World);
  }

  private void HandlePlayerInput()
  {
    // For the sake of simplicity, we'll only concern ourselves with one touch.
    // This has the side effect of a secondary touch overriding the existing one.
    foreach (Touch touch in Input.touches)
    {
      switch (touch.phase)
      {
        case TouchPhase.Began:
          activeTouch = touch;
          break;
          // If the touch is a moving touch, rotate the player camera.
        case TouchPhase.Moved:
          Vector2 delta = (activeTouch.position - touch.position);
          delta.x = Mathf.Clamp(delta.x, minRotationAngle, maxRotationAngle) * rotationSpeed * Time.deltaTime;
          delta.y = delta.y * rotationSpeed * Time.deltaTime;
          playerCamera.transform.eulerAngles = new Vector3(delta.x, delta.y, 0f);
          //playerCamera.transform.rotation = Quaternion.Euler(delta.x, delta.y, 0f);
          break;
          // Clean up state once the touch has ended.
        case TouchPhase.Ended:
          activeTouch = EmptyTouch;
          break;
      }
    }
  }
}
