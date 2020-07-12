using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will translate it's parent gameobject
/// to follow the specified player gameobject with
/// an added offset.
/// </summary>
public class FollowPlayer : MonoBehaviour
{
  [SerializeField] private GameObject player = null;
  [SerializeField] private Vector3 offset = Vector3.zero;

  void Start()
  {
    offset = transform.position - player.transform.position;
  }

  void Update()
  {
    transform.position = player.transform.position + offset;
    // TODO; fix rotation.
  }
}
