using System.Collections;
using UnityEngine;

public class EnemyAttackCombo : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        StartCoroutine(AttackLoop());
    }
    IEnumerator AttackLoop()
    {
        while (!isDead)
        {
            // Triple ataque (un solo trigger)
            animator.SetTrigger("TripleAttack");
            yield return new WaitForSeconds(4f); // Espera tras el triple ataque (ajusta según la animación)

            // Tres ataques simples
            for (int i = 0; i < 3; i++)
            {
                animator.SetTrigger("Attack");
                yield return new WaitForSeconds(3f); // Espera entre ataques simples
            }
        }
    }
}
