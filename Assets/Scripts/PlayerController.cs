using UnityEngine;

/// <summary>
/// This class moves the player based on user input. 
/// </summary>
public class PlayerController : MonoBehaviour
{
  [SerializeField] private PlayerInputManager playerInputManager = null;
  [SerializeField] private Rigidbody playerBody = null;
  [SerializeField] private float speed = 5f;
  [SerializeField] private float rotationSpeed = 0.2f;
  [SerializeField] private float minRotationAngle = -30;
  [SerializeField] private float maxRotationAngle = 50f;
  /// <summary>
  /// If true, the player will pitch upwards when the
  /// user swipes up and downwards when the user swipes
  /// down. If false, the player will pitch in the
  /// opposite direction of the swipe.
  /// </summary>
  [SerializeField] private bool useNaturalMotion = true;

  private void FixedUpdate()
  {
    if (!GameManager.Instance.IsGameActive) return;

    // Move the player at a constant speed. We take into account
    // any rotation that was applied in the Update loop.
    playerBody.velocity = transform.forward * speed;
  }

  private void Update()
  {
    if (!GameManager.Instance.IsGameActive) return;

    // Rotate the player based on user input.
    Vector2 delta = playerInputManager.GetChangeInPlayerInput();
    float direction = useNaturalMotion ? -1f : 1f;
    transform.Rotate(Vector3.up * (delta.x * rotationSpeed * Time.deltaTime));
    transform.Rotate(Vector3.right * (delta.y * rotationSpeed * Time.deltaTime * direction));
  }

  private void LateUpdate()
  {
    // Clamp the player's rotation to the specified range.
    Vector3 currentRotation = transform.eulerAngles;
    currentRotation.x = MathfExtensions.ClampAngle(currentRotation.x, minRotationAngle, maxRotationAngle);
    currentRotation.z = 0f;
    transform.rotation = Quaternion.Euler(currentRotation);
  }
}
