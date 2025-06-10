using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    
    private MoveController moveController;
    private bool isAttacking = false;
    private bool canCancelAttack = false;
    private bool isBlockingPressed = false;

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
    }

    public void DisableAttackCancellation()
    {
        canCancelAttack = false;
    }
    public void StopAttack()
    {
        isAttacking = false;
        canCancelAttack = false;
        moveController.EnableMovement();
        if (isBlockingPressed)
        {
            TryToBlock();
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
