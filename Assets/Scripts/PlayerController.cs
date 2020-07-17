using System.Collections;
using UnityEngine;

/// <summary>
/// This class moves the player based on user input. 
/// </summary>
public class PlayerController : MonoBehaviour
{
  [SerializeField] private PlayerInputManager playerInputManager = null;
  [SerializeField] private Rigidbody playerBody = null;
  /// <summary>
  /// This is used to reset the player's position and rotation when the
  /// game starts over.
  /// </summary>
  [SerializeField] private Transform playerStartPoint = null;
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

  private bool isActive = false;

  private void Awake()
  {
    DontDestroyOnLoad(gameObject);
    GameManager.OnGameReset += ResetPlayer;
    GameManager.OnGameStarted += SetPlayerActive;
    GameManager.OnGameOver += SetPlayerInactive;
  }

  private void ResetPlayer()
  {
    isActive = false;
    playerBody.velocity = Vector3.zero;
    transform.rotation = playerStartPoint.rotation;
    transform.position = playerStartPoint.position;
  }

  private void SetPlayerActive()
  {
    isActive = true;
  }

  private void SetPlayerInactive()
  {
    playerBody.velocity = Vector3.zero;
    isActive = false;
  }

  private void FixedUpdate()
  {
    if (!isActive) return;

    // Move the player at a constant speed. We take into account
    // any rotation that was applied in the Update loop.
    playerBody.velocity = transform.forward * speed;
  }

  private void Update()
  {
    if (!isActive) return;

    // Rotate the player based on user input.
    Vector2 delta = playerInputManager.GetChangeInPlayerInput();
    float direction = useNaturalMotion ? -1f : 1f;
    transform.Rotate(Vector3.up * (delta.x * rotationSpeed * Time.deltaTime));
    transform.Rotate(Vector3.right * (delta.y * rotationSpeed * Time.deltaTime * direction));
  }

  private void LateUpdate()
  {
    if (!isActive) return;

    // Clamp the player's rotation to the specified range.
    Vector3 currentRotation = transform.eulerAngles;
    currentRotation.x = MathfExtensions.ClampAngle(currentRotation.x, minRotationAngle, maxRotationAngle);
    currentRotation.z = 0f;
    transform.rotation = Quaternion.Euler(currentRotation);
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Enemy"))
    {
      EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
      if (enemyController != null && !enemyController.IsDead)
      {
        // Stop movement for a bit.
        playerBody.detectCollisions = false;
        playerBody.isKinematic = true;
        StartCoroutine(AnimatePlayerCollision());
      }
    }
  }

  private IEnumerator AnimatePlayerCollision()
  {
    yield return new WaitForSeconds(.5f);
    playerBody.detectCollisions = true;
    playerBody.isKinematic = false;
  }

  private void OnDestroy()
  {
    StopAllCoroutines();
    GameManager.OnGameReset -= ResetPlayer;
    GameManager.OnGameStarted -= SetPlayerActive;
    GameManager.OnGameOver -= SetPlayerInactive;
  }
}
