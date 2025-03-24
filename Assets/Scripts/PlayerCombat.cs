using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;
    private bool canAttack = true;



    
    private void OnEnable()
    {
        InputManager.OnAttackPressed += TryToAttack;
    }

    private void OnDisable()
    {
        InputManager.OnAttackPressed -= TryToAttack;
    }

    private void TryToAttack()
    {
        print("Intento de ataque"); 
        if (!canAttack) return;
        StartCoroutine(AttackCooldown());
    }
    IEnumerator AttackCooldown()
    {
        playerAnim.SetTrigger("Attack");
        canAttack = false;
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
    }
}
