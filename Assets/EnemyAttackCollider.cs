using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] private BasicEnemy enemyScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeHit(enemyScript.damage);
        }
    }
}
