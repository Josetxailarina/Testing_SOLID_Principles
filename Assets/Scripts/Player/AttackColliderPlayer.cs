using UnityEngine;

public class AttackColliderPlayer : MonoBehaviour
{
    [SerializeField] private PlayerState playerState;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            bool isParrying = damageable.TakeHit(playerState.damage);

            if (isParrying)
            {
               playerState.AddPosture(playerState.damage * 2);
            }
        }
    }
}
