using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;

    private MoveController moveController;

    private bool isAttacking = false;
    public bool isBlocking = false;
    public bool isParrying = false;

    private bool canCancelAttack = false;
    private bool isBlockingPressed = false;
    private bool attackBufferActive = false;
    private bool isAttackPressed = false;

    private float attackPressedTimer = 0;
    private float timeToActivateChargedAttack = 0.5f;

    // Parry variables
    [SerializeField] private float parryWindowDuration = 0.3f;
    private float parryWindowTimer = 0f;

    private void OnEnable()
    {
        InputManager.OnAttackPressed += AttackPressed;
        InputManager.OnAttackCanceled += AttackReleased;
        InputManager.OnDodgePressed += TryToDodge;
        InputManager.OnBlockPressed += TryToBlock;
        InputManager.OnBlockCanceled += StopBlocking;
    }

    private void OnDisable()
    {
        InputManager.OnAttackPressed -= AttackPressed;
        InputManager.OnAttackCanceled -= AttackReleased;
        InputManager.OnDodgePressed -= TryToDodge;
        InputManager.OnBlockPressed -= TryToBlock;
        InputManager.OnBlockCanceled -= StopBlocking;
    }

    private void Start()
    {
        moveController = GetComponent<MoveController>();
    }

    private void Update()
    {
        // L�gica de la ventana de parry
        ManageParryWindow();

        if (isAttackPressed && attackPressedTimer < timeToActivateChargedAttack)
        {
            attackPressedTimer += Time.deltaTime;
            if (attackPressedTimer >= timeToActivateChargedAttack)
            {
                TryToAttack(true);
            }
        }
    }

    private void ManageParryWindow()
    {
        if (isParrying)
        {
            parryWindowTimer -= Time.deltaTime;
            if (parryWindowTimer <= 0f)
            {
                isParrying = false;
            }
        }
    }

    private void AttackPressed()
    {
        isAttackPressed = true;
        attackPressedTimer = 0f; 
    }
    private void AttackReleased()
    {
        isAttackPressed = false;
        if (attackPressedTimer < timeToActivateChargedAttack)
        {
            TryToAttack(false);
        }
        attackPressedTimer = 0f;
    }

    private void TryToAttack(bool isCharged)
    {
        if (!isAttacking)
        {
            moveController.UpdateSpriteDirection();
            moveController.StopMovement();
            isAttacking = true;
            canCancelAttack = true;
            if (isCharged)
            {
                playerAnim.SetTrigger("ChargedAttack");
            }
            else
            {
                playerAnim.SetTrigger("Attack");
            }
        }
        else
        {
            attackBufferActive = true;
        }
    }

    public void DisableAttackCancellation() //called by the animation event when cancelAttack windows are closed
    {
        canCancelAttack = false;
    }
    public void StopAttack() //called by the animation event when the attack animation ends
    {
        isAttacking = false;
        canCancelAttack = false;
        moveController.EnableMovement();
        if (isBlockingPressed)
        {
            TryToBlock();
        }
        if (attackBufferActive)
        {
            attackBufferActive = false;
            TryToAttack(false);
        }
    }

    private void TryToDodge()
    {

    }
    private void TryToBlock()
    {
        isBlockingPressed = true;
        if (!isAttacking || canCancelAttack)
        {
            attackBufferActive = false;
            isAttacking = false;
            canCancelAttack = false;
            playerAnim.SetTrigger("GoBlock");
            playerAnim.SetBool("IsBlocking", true);
            isBlocking = true;
            moveController.StopMovement();

            // Inicia la ventana de parry
            isParrying = true;
            parryWindowTimer = parryWindowDuration;
        }
    }
    private void StopBlocking()
    {
        isBlockingPressed = false;
        if (playerAnim.GetBool("IsBlocking"))
        {
            moveController.EnableMovement();
        }
        playerAnim.SetBool("IsBlocking", false);
        isBlocking = false;

        // Resetea el parry al dejar de bloquear
        isParrying = false;
        parryWindowTimer = 0f;
    }

}
