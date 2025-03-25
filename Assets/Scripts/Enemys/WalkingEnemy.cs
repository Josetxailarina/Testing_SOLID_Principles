using System.Collections;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    private bool movingRight = false;
    [SerializeField] private float moveVelocity = 10f;
    private Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
    }
    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        if (!isDead)
        {
            if (movingRight)
            {
                rb.linearVelocity = new Vector2(moveVelocity, rb.linearVelocity.y);
            }
            else
            {
                rb.linearVelocity = new Vector2(-moveVelocity, rb.linearVelocity.y);
            } 
        }
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(2f);
        movingRight = !movingRight;
        StartCoroutine(ChangeDirection());
    }
}
