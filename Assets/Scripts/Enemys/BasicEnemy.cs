using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour

{
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth = 100f; 
    public float damage = 10f;
    [SerializeField] private Collider2D hitboxCollider;
    [SerializeField] private GameObject spritesParent;
    [SerializeField] protected HealthBar healthBar; 

    protected SpriteRenderer[] spriteRenderers;
    [HideInInspector] public bool isDead = false;



    public virtual void Start() //virtual = can be overriden by the child class
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        healthBar.UpdateHealthBars(currentHealth, maxHealth);
    }
    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            isDead = true;
            StopAllCoroutines();
            spritesParent.SetActive(false);
        }
        else
        {
            StartCoroutine(FlashRed());
            healthBar.ReduceHealth(currentHealth, maxHealth);
        }
    }

    protected IEnumerator FlashRed()
    {
        SetTintColor(new Color(1, 1, 1, 1)); // White with alpha 1
        hitboxCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        hitboxCollider.enabled = true;
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
