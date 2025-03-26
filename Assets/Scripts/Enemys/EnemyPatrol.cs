using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private bool movingRight;
    [SerializeField] private float moveVelocity = 10f;
    [SerializeField] private float timeToChangeState = 2f;
    [SerializeField] private Animator spriteAnimator;
    private Rigidbody2D rb;
    private BasicEnemy basicEnemy;
    private bool moving = true;

    private void Start()
    {
        basicEnemy = GetComponent<BasicEnemy>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
        spriteAnimator.SetBool("Walking", true);
        if (!movingRight)
        {
            FlipSprite();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        if (!basicEnemy.isDead && moving)
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
    private void FlipSprite()
    {
        Vector3 localScale = spriteAnimator.transform.localScale;
        localScale.x *= -1;
        spriteAnimator.transform.localScale = localScale;
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(timeToChangeState);
        
        //STOP MOVING
        spriteAnimator.SetBool("Walking", false);
        moving = false;

        yield return new WaitForSeconds(timeToChangeState);

        //START MOVING
        spriteAnimator.SetBool("Walking", true);
        movingRight = !movingRight;
        FlipSprite();
        moving = true;
        if (!basicEnemy.isDead)
        {
            StartCoroutine(ChangeDirection());
        }
    }
}
