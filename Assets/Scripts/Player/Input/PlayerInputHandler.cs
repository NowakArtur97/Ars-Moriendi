using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 movementInput;

    public void OnMoveInput(CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log("Move input: " + movementInput);
    }

    public void OnJumpInput(CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("started");
        }
        if (context.performed)
        {
            Debug.Log("performed");
        }
        if (context.canceled)
        {
            Debug.Log("canceled");
        }
    }
}
