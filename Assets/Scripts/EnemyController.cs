using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public bool IsDead { get; private set; }
  
  [SerializeField] private Rigidbody enemyRigidBody;
  [SerializeField] private MeshRenderer enemyMeshRenderer;
  [SerializeField] private Material deathMaterial;

  private void Start()
  {
    IsDead = false;
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (!IsDead && collision.gameObject.CompareTag("Player"))
    {
      enemyRigidBody.AddForce(collision.impulse * 100f, ForceMode.Force);
      enemyMeshRenderer.material = deathMaterial;
      IsDead = true;
    }
  }
}