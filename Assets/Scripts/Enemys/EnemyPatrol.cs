using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private bool movingRight;
    [SerializeField] private float moveVelocity = 10f;
    [SerializeField] private float timeToChangeDirection = 2f;
    private Rigidbody2D rb;
    private BasicEnemy basicEnemy;

    private void Start()
    {
        basicEnemy = GetComponent<BasicEnemy>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirection());
    }
    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        if (!basicEnemy.isDead)
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
        yield return new WaitForSeconds(timeToChangeDirection);
        movingRight = !movingRight;
        StartCoroutine(ChangeDirection());
    }
}
