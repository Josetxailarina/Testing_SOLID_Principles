using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Animator playerAnim;

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
        //The animator controlls the collider state and the combo
        playerAnim.SetTrigger("Attack");
    }
  
}
