using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] private PhysicsMaterial2D airPhysicsMaterial;
    [SerializeField] private PhysicsMaterial2D groundPhysicsMaterial;
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private float dashAttackDistance = 2f;
    [SerializeField] private float dashAttackduration = 0.2f;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnEnable()
    {
        InputManager.OnJumpPressed += TryToJump;
        InputManager.OnJumpCanceled += JumpCanceled;
    }

    private void OnDisable()
    {
        InputManager.OnJumpPressed -= TryToJump;
        InputManager.OnJumpCanceled -= JumpCanceled;
    }

    public void StopMovement()
    {
        canMove = false;
        if (onGround)
        {
            rb.angularVelocity = 0;
        }
    }
    public void EnableMovement()
    {
        canMove = true;
    }
    public void UpdateSpriteDirection()
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
        rb.sharedMaterial = airPhysicsMaterial;

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
        rb.sharedMaterial = groundPhysicsMaterial;
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
        rb.sharedMaterial = airPhysicsMaterial;
    }




    private void Update()
    {
        moveDirection = playerInput.actions["Move"].ReadValue<Vector2>().x;
        if (canMove)
        {
            anim.SetFloat("Velocity", Mathf.Abs(moveDirection));
            UpdateSpriteDirection(); 
        }

        UpdateJumpBufferTimer();
        UpdateCoyoteTimer();
    }
    private void FixedUpdate()
    {

        if (canMove)
        {
            rb.linearVelocity = new Vector2(moveDirection * speed, rb.linearVelocity.y);

        }
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

    public void DashAttack()
    {
        if (onGround)
        {
            StopAllCoroutines();
            StartCoroutine(DashAttackCoroutine(dashAttackDistance, dashAttackduration)); 
        }
    }

    private IEnumerator DashAttackCoroutine(float distance, float duration)
    {
        float elapsed = 0f;
        int direction = lookingRight ? 1 : -1;
        Vector2 start = rb.position;
        Vector2 end = start + new Vector2(direction * distance, 0);

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            rb.MovePosition(Vector2.Lerp(start, end, t));
            elapsed += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        rb.MovePosition(end);
    }

}
