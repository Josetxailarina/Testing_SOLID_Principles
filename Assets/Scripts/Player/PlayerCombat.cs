using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    
    private MoveController moveController;
    private bool isAttacking = false;
    private bool canCancelAttack = false;
    private bool isBlockingPressed = false;
    private bool attackBufferActive = false;

    private void OnEnable()
    {
        InputManager.OnAttackPressed += TryToAttack;
        InputManager.OnDodgePressed += TryToDodge;
        InputManager.OnBlockPressed += TryToBlock;
        InputManager.OnBlockCanceled += StopBlocking;
    }

    private void OnDisable()
    {
        InputManager.OnAttackPressed -= TryToAttack;
        InputManager.OnDodgePressed -= TryToDodge;
        InputManager.OnBlockPressed -= TryToBlock;
        InputManager.OnBlockCanceled -= StopBlocking;
    }

    private void Start()
    {
        moveController = GetComponent<MoveController>();
    }

    private void TryToAttack()
    {
        if (!isAttacking)
        {
            moveController.StopMovement();
            isAttacking = true;
            canCancelAttack = true;
            playerAnim.SetTrigger("Attack"); 
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
            TryToAttack();
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
            moveController.StopMovement();
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
    }

}
