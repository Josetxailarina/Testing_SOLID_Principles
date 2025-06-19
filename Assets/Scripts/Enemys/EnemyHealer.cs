using UnityEngine;

public class EnemyHealer : BasicEnemy
{
    [SerializeField] private float healAmount = 1f;
    [SerializeField] private ParticleSystem healEffect;
    private bool isHealingEffectPlaying = false;


    public override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        if (!isDead && currentHealth < maxHealth)
        {
            if (!isHealingEffectPlaying)
            {
                healEffect.Play();
                isHealingEffectPlaying = true;
            }
            currentHealth += healAmount * Time.deltaTime;
            healthBar.UpdateRedBar(currentHealth, maxHealth);
        }
        else if (isHealingEffectPlaying)
        {
            healEffect.Stop();
            isHealingEffectPlaying = false;
        }
    }
}
