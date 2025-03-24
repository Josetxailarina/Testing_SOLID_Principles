using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable

{
    [SerializeField] private float health = 100f;
    private SpriteRenderer spriteRenderer;
    Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        FlashRed();
        if (health <= 0)
        {
            StopAllCoroutines();
            spriteRenderer.enabled = false;   
            coll.enabled = false;
            
        }

    }
    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.white;
        coll.enabled = false;

        yield return new WaitForSeconds(0.1f);
        coll.enabled = true;

        spriteRenderer.color = Color.red;
    }
}
