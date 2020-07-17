using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public bool IsDead
  {
    get { return enemyStatus == EnemyStatus.Dead; }
  }

  public bool IsAlive
  {
    get { return enemyStatus == EnemyStatus.Alive; }
  }

  [SerializeField] private Rigidbody enemyRigidBody = null;
  [SerializeField] private MeshRenderer enemyMeshRenderer = null;
  [SerializeField] private Material deathMaterial = null;
  [SerializeField] private float deathImpulseMultiplier = 200f;

  private EnemyStatus enemyStatus = EnemyStatus.Alive;

  private void OnCollisionEnter(Collision collision)
  {
    if (IsAlive && collision.gameObject.CompareTag("Player"))
    {
      enemyRigidBody.AddForce(collision.impulse * deathImpulseMultiplier, ForceMode.Force);
      enemyMeshRenderer.material = deathMaterial;
      enemyStatus = EnemyStatus.SetToDie;
      StartCoroutine(KillEnemy());
    }
  }

  private IEnumerator KillEnemy()
  {
    // Wait for a second so the transition is not so jarring.
    yield return new WaitForSeconds(1f);
    enemyRigidBody.isKinematic = true;
    enemyStatus = EnemyStatus.Dead;
  }

  private void OnDestroy()
  {
    enemyStatus = EnemyStatus.Dead;
    StopAllCoroutines();
  }
}