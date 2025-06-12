using UnityEngine;

public class EnemyHealer : BasicEnemy
{
    [SerializeField] private float healAmount = 1f;
    private float initialHealth;

    public override void Start()
    {
        base.Start();
        initialHealth = currentHealth;
    }
    private void Update()
    {
        if (!isDead && currentHealth < initialHealth)
        {
            currentHealth += healAmount * Time.deltaTime;
            healthBar.UpdateRedBar(currentHealth, maxHealth);
        }    
    }
}
