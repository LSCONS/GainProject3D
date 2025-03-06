using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Vector2 playerMoveDir;
    public Vector2 PlayerMoveDir { get => playerMoveDir; }

    private Vector2 mousePosition;
    public Vector2 MousePosition { get => mousePosition; }

    private bool isJump;
    public bool IsJump { get => isJump; }

    PlayerAction input;

    private void OnEnable()
    {
        input = new PlayerAction();
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += StopMove;
        input.Player.Jump.performed += OnJump;
        input.Player.Jump.canceled += StopJump;
        input.Player.MousePosition.performed += OnMousePosition;

        input.Enable();
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= StopMove;
        input.Player.Jump.performed -= OnJump;
        input.Player.Jump.canceled -= StopJump;
        input.Player.MousePosition.performed -= OnMousePosition;

        input.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        playerMoveDir = context.ReadValue<Vector2>().normalized;
        Debug.Log("움직이나요?");
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        playerMoveDir = Vector2.zero;
        Debug.Log("멈췄어요?");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        isJump = true;
        Debug.Log("점프했나요?");
    }

    private void StopJump(InputAction.CallbackContext context)
    {
        isJump = false;
        Debug.Log("점프그만둬요?");
    }

    private void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}
