using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private int groundLayer = 0;
    [SerializeField] private MoveController moveController;

    private void Start()
    {
        groundLayer = LayerMask.NameToLayer("Ground");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
           moveController.TouchGround();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            moveController.LeaveGround();
        }
    }
}
