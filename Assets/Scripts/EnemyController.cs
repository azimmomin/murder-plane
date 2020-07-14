using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public bool IsDead { get; private set; }

  private void Start()
  {
    IsDead = false;
  }
}
