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
    if (!IsDead && other.gameObject.tag == "Player")
      IsDead = true;
  }
}