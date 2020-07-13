﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class moves the player based on user input. 
/// </summary>
public class PlayerController : MonoBehaviour
{
  [SerializeField] private PlayerInputManager playerInputManager = null;
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

  private bool shouldStopMovement = false;
  private void Update()
  {
    // Move the player forward at a constant speed
    if (!shouldStopMovement)
    transform.Translate(0f, 0f, speed * Time.deltaTime);

    // Rotate the player based on user input.
    Vector2 delta = playerInputManager.GetChangeInPlayerInput();
    transform.Rotate(Vector3.up * (delta.x * rotationSpeed * Time.deltaTime));
    float direction = useNaturalMotion ? -1f : 1f;
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

  private void OnCollisionEnter(Collision collisionInfo)
  {
    shouldStopMovement = true;
    Debug.LogError($"Trigger Entered -- {collisionInfo.collider.gameObject.tag}");
  }

  private void OnTriggerEnter(Collider other)
  {
    shouldStopMovement = true;
    Debug.LogError($"Trigger Entered -- {other.gameObject.tag}");
  }
}
