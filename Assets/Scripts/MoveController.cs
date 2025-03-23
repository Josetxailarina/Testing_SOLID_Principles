using UnityEngine;

public class MoveController : MonoBehaviour
{
    private float moveDirection = 0;
    private float jumpBufferTimer = 0;
    private float coyoteTime = 0;
    [HideInInspector] public bool jumping = false;
    [HideInInspector] public bool onGround = false;
    private bool jumpPressed = false;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;


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
    }
    private void TryToJump()
    {
        jumpPressed = true;
        if (coyoteTime > 0 && !jumping)
        {
            PerformJump();
        }
        else if (coyoteTime <= 0)
        {
            jumpBufferTimer = 0.15f;
        }
    }
    private void JumpCanceled()
    {
        jumpPressed = false;
        if (jumping && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f); // Reducir la altura del salto si el botón se suelta temprano
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * 0.5f);

        }
    }
    public void TouchGround()
    {
        onGround = true;
        jumping = false;
        coyoteTime = 0.1f;
        if (jumpBufferTimer > 0)
        {
            PerformJump();
        }
    }
    public void LeaveGround()
    {
        onGround = false;
        coyoteTime = 0.1f;

    }

    
    

    private void Update()
    {
        rb.linearVelocity = new Vector2(moveDirection * speed , rb.linearVelocity.y);
        if (!onGround && jumpBufferTimer > 0)
        {
            jumpBufferTimer -= Time.deltaTime;
        }
        if (!onGround && coyoteTime > 0)
        {
            coyoteTime -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (rb.linearVelocity.y < 2 && coyoteTime <= 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (1.5f) * Time.deltaTime; // Aumentar la gravedad para caídas más rápidas
        }
    }

}
