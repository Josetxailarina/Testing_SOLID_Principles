using System.Collections;
using UnityEngine;

public class BasicEnemy : MonoBehaviour

{                  
    [SerializeField] protected float health = 100f; //protected = accessed by the child class
    public float damage = 10f;
    [SerializeField] private Collider2D hitboxCollider;
    [SerializeField] private GameObject spritesParent;

    protected SpriteRenderer[] spriteRenderers;
    [HideInInspector] public bool isDead = false;



    public virtual void Start() //virtual = can be overriden by the child class
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }
    public virtual void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            isDead = true;
            StopAllCoroutines();
            spritesParent.SetActive(false);
        }
        else
        {
            StartCoroutine(FlashRed());
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
