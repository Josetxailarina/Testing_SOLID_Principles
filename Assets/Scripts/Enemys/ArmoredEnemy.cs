using Unity.Mathematics;
using UnityEngine;

public class ArmoredEnemy : BasicEnemy
{
    private float armor = 100f;
    
    public override void TakeDamage(float damage)
    {
        if (armor > 0)
        {
            StartCoroutine(FlashRed());
            armor -= damage;
            if(armor <= 0)
            {
                base.TakeDamage(-armor);
            }
        }
        else
        {
            base.TakeDamage(damage);
        }
    }

}
