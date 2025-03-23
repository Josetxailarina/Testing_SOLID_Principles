using UnityEngine;

public class MoveController : MonoBehaviour
{
    private float moveDirection = 0;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        InputManager.OnMovePressed += GetDirection;
    }

    private void OnDisable()
    {
        InputManager.OnMovePressed -= GetDirection;
    }

    private void GetDirection(float direction)
    {
        moveDirection = direction;
    }

    private void Update()
    {
        rb.linearVelocity = new Vector2(moveDirection * speed , rb.linearVelocity.y);
    }

}
