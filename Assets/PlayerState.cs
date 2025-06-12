using UnityEngine;

public class PlayerState : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float maxPosture = 100f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private ParticleSystem blockEffect;
    [SerializeField] private ParticleSystem parryEffect;
    private PlayerCombat playerCombat;
    private float currentHealth;
    private float currentPosture;


    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBars(currentHealth, maxHealth);
        playerCombat = GetComponent<PlayerCombat>();
    }
   
    public void TakeHit(float damage)
    {
        if (playerCombat.isParrying)
        {
            parryEffect.Play();
            SoundsManager.Instance.PlayRandomParry();
        }
        else if (playerCombat.isBlocking)
        {
            blockEffect.Play();
            SoundsManager.Instance.PlayRandomBlock();
        }
        else
        {
            ReduceHealth(damage);
        }
    }

    private void ReduceHealth(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBar.ReduceHealth(currentHealth, maxHealth);
        bloodEffect.Play();
        SoundsManager.Instance.bloodHitSound.Play();
    }
}

   
