using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour

{                  
    [SerializeField] protected float health = 100f; //protected = accessed by the child class
    [SerializeField] private float damage = 10f;
    [SerializeField] private Collider2D hitboxCollider;

    
    protected SpriteRenderer spriteRenderer;
    protected Color actualColor;
    protected bool isDead = false;



    public virtual void Start() //virtual = can be overriden by the child class
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        actualColor = spriteRenderer.color;
    }
    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            isDead = true;
            StopAllCoroutines();
            spriteRenderer.enabled = false;
            hitboxCollider.enabled = false;
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }
    
    protected IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.white;
        hitboxCollider.enabled = false;
        yield return new WaitForSeconds(0.1f);
        hitboxCollider.enabled = true;
        spriteRenderer.color = actualColor;
    }
    
}
