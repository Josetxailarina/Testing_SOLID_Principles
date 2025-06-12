using UnityEngine;

public class EnemyHitbox : MonoBehaviour,IDamageable
{
    public BasicEnemy enemyScript;
    public void TakeHit(float amount)
    {
        enemyScript.TakeDamage(amount);
    }
   
}
