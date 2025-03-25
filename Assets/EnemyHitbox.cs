using UnityEngine;

public class EnemyHitbox : MonoBehaviour,IDamageable
{
    public Enemy enemyScript;
    public void TakeDamage(float amount)
    {
        enemyScript.TakeDamage(amount);
    }
   
}
