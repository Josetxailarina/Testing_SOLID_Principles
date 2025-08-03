using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable

{
    public float maxHealth { get; private set; }
    public float currentHealth { get; protected set; }

    public float damage = 10f;
    private bool canTakeDamage = true;
    private bool isParrying;

    [SerializeField] private GameObject spritesParent;
    [SerializeField] protected HealthBar healthBar;
    PostureHandler postureHandler;

    protected SpriteRenderer[] spriteRenderers;
    [HideInInspector] public bool isDead = false;
    private GameObject playerObject;
    private Rigidbody2D rb;

    private void Awake()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
        postureHandler = GetComponent<PostureHandler>();
        rb = GetComponent<Rigidbody2D>();

    }

    public virtual void Start() 
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        healthBar.UpdateHealthBars(currentHealth, maxHealth);
    }
    public void AddPosture(float amount)
    {
        postureHandler.AddPosture(amount);
    }
    public bool TakeHit(float amount)
    {
        if (isParrying)
        {
            // Parry effects
            return true;
        }
        else if (canTakeDamage && !isDead)
        {
            TakeDamage(amount);
            AddPosture(amount/2);
            return false;
        }
        else
        {
            return false;
        }
    }
    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        SoundsManager.Instance.bloodHitSound.Play();

        if (currentHealth <= 0)
        {
            isDead = true;
            StopAllCoroutines();
            spritesParent.SetActive(false);
        }
        else
        {
            knockUp();
            StartCoroutine(FlashDamageEffect());
            healthBar.ReduceHealth(currentHealth, maxHealth);
        }
    }

    public void GetStunned()
    {

    }
    public void knockUp()
    {
        if (playerObject != null)
        {
            float directionX = Mathf.Sign(transform.position.x - playerObject.transform.position.x);
            Vector2 force = new Vector2(directionX, 0f).normalized * 3f;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    protected IEnumerator FlashDamageEffect()
    {
        SetTintColor(new Color(1, 1, 1, 1)); // White with alpha 1
        canTakeDamage = false;
        yield return new WaitForSeconds(0.1f);
        canTakeDamage = true;
        SetTintColor(new Color(1, 1, 1, 0)); // White with alpha 0
    }

    private void SetTintColor(Color color)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.material.SetColor("_Tint", color);
        }
    }

}
