using UnityEngine;

public class EnemyHealer : BasicEnemy
{
    [SerializeField] private float healAmount = 1f;
    private float initialHealth;

    public override void Start()
    {
        base.Start();
        initialHealth = health;
    }
    private void Update()
    {
        if (!isDead && health < initialHealth)
        {
            health += healAmount * Time.deltaTime;
        }    
    }
}
