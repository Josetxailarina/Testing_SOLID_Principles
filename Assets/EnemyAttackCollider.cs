using UnityEngine;

public class EnemyAttackCollider : MonoBehaviour
{
    [SerializeField] private BasicEnemy enemyScript;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            bool isParrying = damageable.TakeHit(enemyScript.damage);

            if (isParrying)
            {
                enemyScript.AddPosture(enemyScript.damage * 2);
            }
        }
    }
}
