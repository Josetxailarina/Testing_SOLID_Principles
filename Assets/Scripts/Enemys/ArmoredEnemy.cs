using Unity.Mathematics;
using UnityEngine;

public class ArmoredEnemy : BasicEnemy
{
    private float armor = 100f;
    [SerializeField] private Color armorColor;
    private Color originalColorRef;

    public override void Start()
    {
        base.Start();
        originalColorRef = spriteRenderer.color;
        spriteRenderer.color = armorColor;
        actualColor = armorColor;
    }

    public override void TakeDamage(float damage)
    {
        if (armor > 0)
        {
            StartCoroutine(FlashRed());
            armor -= damage;
            if(armor <= 0)
            {
                base.TakeDamage(-armor);
                actualColor = originalColorRef;
            }
        }
        else
        {
            base.TakeDamage(damage);
        }
    }

}
