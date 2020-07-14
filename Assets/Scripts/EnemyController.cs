using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public bool IsDead { get; private set; }

  private void Start()
  {
    IsDead = false;
  }

  private void OnTriggerEnter(Collider other)
  {
    // TODO: Apply force to enemy rigidbody
    if (!IsDead && other.gameObject.tag == "Player")
      IsDead = true;
  }
}