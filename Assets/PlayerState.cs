using UnityEngine;

public class PlayerState : MonoBehaviour, IDamageable
{
    // IDamageable interface implementation
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    //

    [SerializeField] public float damage = 25f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ParticleSystem bloodEffect;
    [SerializeField] private ParticleSystem blockEffect;
    [SerializeField] private ParticleSystem parryEffect;
    private PostureHandler postureHandler;
    private PlayerCombat playerCombat;


    private void Awake()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
        healthBar.UpdateHealthBars(currentHealth, maxHealth);
        playerCombat = GetComponent<PlayerCombat>();
        postureHandler = GetComponent<PostureHandler>();
    }
   
    public void AddPosture(float amount)
    {
        postureHandler.AddPosture(amount);
    }
    public bool TakeHit(float damage)
    {
        if (playerCombat.isParrying)
        {
            parryEffect.Play();
            SoundsManager.Instance.PlayRandomParry();
            return true;
        }
        else if (playerCombat.isBlocking)
        {
            blockEffect.Play();
            AddPosture(damage);
            SoundsManager.Instance.PlayRandomBlock();
            return false;
        }
        else
        {
            ReduceHealth(damage);
            AddPosture(damage / 2);
            return false;
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

   
