using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    [SerializeField] private PlayerCombat playerCombat;

    public void DisableAttackCancellation()
    {
        playerCombat.DisableAttackCancellation();
    }
    public void EndAttack()
    {
        playerCombat.StopAttack();
    }
    public void PlayRandomSwing()
    {
        SoundsManager.Instance.PlayRandomSwing();
    }
}
