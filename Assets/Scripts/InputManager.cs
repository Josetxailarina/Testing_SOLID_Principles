using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private float moveDirection = 0;
    public static event Action<float> OnMovePressed;

    // Maneja la entrada de movimiento
    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>().x;
            OnMovePressed?.Invoke(moveDirection);
            Debug.Log("Moviendo a " + moveDirection);
        }
        else if (context.canceled)
        {
            moveDirection = 0;
            OnMovePressed?.Invoke(moveDirection);
            Debug.Log("Parado");
        }
    }
}
