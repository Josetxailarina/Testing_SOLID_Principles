using UnityEngine;

public class EnemyHitbox : MonoBehaviour,IDamageable
{
    public BasicEnemy enemyScript;
    public void TakeDamage(float amount)
    {
        enemyScript.TakeDamage(amount);
    }
   
}
