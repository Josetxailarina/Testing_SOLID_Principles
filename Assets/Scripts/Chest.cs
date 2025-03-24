using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private SpriteRenderer spriteRenderer;
    private Collider2D _collider;
    [SerializeField] private int coinsAmount = 10;
    public void Interact()
    {
        spriteRenderer.enabled = false;
        _collider.enabled = false;
        Debug.Log($"Player got {coinsAmount} coins");

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

   
}
