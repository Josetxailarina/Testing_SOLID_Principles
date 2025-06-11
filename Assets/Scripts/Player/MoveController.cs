using Unity.VisualScripting;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private float moveDirection = 0;
    private float jumpBufferTimer = 0;
    private float jumpBufferDuration = 0.15f;
    private float coyoteTimer = 0;
    private float coyoteDuration = 0.1f;
    private Rigidbody2D rb;

    private bool lookingRight = true;
    private bool jumpPressed = false;
    
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool jumping = false;
    [HideInInspector] public bool onGround = false;
    
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject spriteObject;
    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable()
    {
        InputManager.OnMovePressed += GetDirection;
        InputManager.OnJumpPressed += TryToJump;
        InputManager.OnJumpCanceled += JumpCanceled;
    }

    private void OnDisable()
    {
        InputManager.OnMovePressed -= GetDirection;
        InputManager.OnJumpPressed -= TryToJump;
        InputManager.OnJumpCanceled -= JumpCanceled;
    }

    private void GetDirection(float direction)
    {
        moveDirection = direction;
        anim.SetFloat("Velocity", Mathf.Abs(moveDirection));
        UpdateSpriteDirection();
    }

    public void StopMovement()
    {
        canMove = false;
    }
    public void EnableMovement()
    {
        canMove = true;
    }
    private void UpdateSpriteDirection()
    {
        if (moveDirection > 0 && !lookingRight)
        {
            spriteObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            lookingRight = true;
        }
        else if (moveDirection < 0 && lookingRight)
        {
            spriteObject.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            lookingRight = false;
        }
    }

    private void TryToJump()
    {
        jumpPressed = true;
        if (coyoteTimer > 0 && !jumping) 
        {
            PerformJump();
        }
        else if (coyoteTimer <= 0)
        {
            jumpBufferTimer = jumpBufferDuration;
        }
    }
    private void JumpCanceled()
    {
        // Reduce the jump height if the jump button is released
        jumpPressed = false;
        if (jumping && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); 
        }
    }
    private void PerformJump()
    {
        jumping = true;
        if (jumpPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else
        {
            // Reduce the jump height if the jump button is not pressed
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.5f);

        }
    }
    public void TouchGround()
    {
        onGround = true;
        jumping = false;
        anim.SetBool("InAir", false);
        coyoteTimer = coyoteDuration;
        if (jumpBufferTimer > 0)
        {
            PerformJump();
        }
    }
    public void LeaveGround()
    {
        onGround = false;
        coyoteTimer = coyoteDuration;
        anim.SetBool("InAir", true);
    }




    private void Update()
    {
        UpdateJumpBufferTimer();
        UpdateCoyoteTimer();
    }
    private void FixedUpdate()
    {
        
        rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);

        // Make the player fall faster
        if (rb.linearVelocity.y < 2 && coyoteTimer <= 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (1.5f) * Time.deltaTime; // Aumentar la gravedad para caídas más rápidas
        }
    }

    private void UpdateCoyoteTimer()
    {
        if (!onGround && coyoteTimer > 0)
        {
            coyoteTimer -= Time.deltaTime;
        }
    }

    private void UpdateJumpBufferTimer()
    {
        if (!onGround && jumpBufferTimer > 0)
        {
            jumpBufferTimer -= Time.deltaTime;
        }
    }

    

}
