using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private float moveDirection = 0;
    public static event Action<float> OnMovePressed;
    public static event Action OnJumpPressed;
    public static event Action OnJumpCanceled;
    public static event Action OnInteractPressed;
    public static event Action OnAttackPressed;

    // CALLED BY THE INPUT SYSTEM   

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAttackPressed?.Invoke();
        }
    }
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>().x;
            if (moveDirection != 0)
            {
                OnMovePressed?.Invoke(moveDirection);
            }
        }
        else if (context.canceled)
        {
            moveDirection = 0;
            OnMovePressed?.Invoke(moveDirection);
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpPressed?.Invoke();
        }
        else if (context.canceled)
        {
           OnJumpCanceled?.Invoke();
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnInteractPressed?.Invoke();
        }
    }
}
