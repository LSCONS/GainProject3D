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

    private PlayerControl playerControl;

    PlayerAction input;

    Coroutine OnInputCoroutine;

    private void OnValidate()
    {
        playerControl = GetComponent<PlayerControl>();
    }

    private void OnEnable()
    {
        input = new PlayerAction();
        input.Player.Move.performed += OnMove;
        input.Player.Move.canceled += StopMove;
        input.Player.Jump.started += OnJump;
        input.Player.Jump.canceled += StopJump;
        input.Player.MousePosition.started += OnMousePosition;
        input.Player.MousePosition.canceled += StopMousePosition;

        input.Enable();
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= OnMove;
        input.Player.Move.canceled -= StopMove;
        input.Player.Jump.started -= OnJump;
        input.Player.Jump.canceled -= StopJump;
        input.Player.MousePosition.started -= OnMousePosition;
        input.Player.MousePosition.canceled -= StopMousePosition;

        input.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        playerMoveDir = context.ReadValue<Vector2>().normalized;

        if(OnInputCoroutine == null)
        {
            OnInputCoroutine = StartCoroutine(FixedUpdateOnInput());
        }
        Debug.Log("움직이나요?");
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        playerMoveDir = Vector2.zero;
        if(OnInputCoroutine != null)
        {
            StopCoroutine(OnInputCoroutine);
            OnInputCoroutine = null;
        }
        playerControl.MoveCharacter();
        Debug.Log("멈췄어요?");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        isJump = true;
        if (OnInputCoroutine == null)
        {
            OnInputCoroutine = StartCoroutine(FixedUpdateOnInput());
        }
        Debug.Log("점프했나요?");
    }


    //TODO: 코드 최적화 가능
    IEnumerator FixedUpdateOnInput()
    {
        while (IsJump || playerMoveDir.magnitude > 0f)
        {
            playerControl.JumpCharacter();
            playerControl.MoveCharacter();
            yield return new WaitForFixedUpdate();
        }
    }


    private void StopJump(InputAction.CallbackContext context)
    {
        isJump = false;
        if (OnInputCoroutine != null)
        {
            StopCoroutine(OnInputCoroutine);
            OnInputCoroutine = null;
        }
        Debug.Log("점프그만둬요?");
    }

    private void OnMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    private void StopMousePosition(InputAction.CallbackContext context)
    {
        mousePosition = Vector2.zero;
    }
}
