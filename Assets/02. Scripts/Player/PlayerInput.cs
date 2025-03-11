using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Vector2 _playerMoveDir;
    public Vector2 PlayerMoveDir { get => _playerMoveDir; }

    private Vector2 _mousePosition;
    public Vector2 MousePosition { get => _mousePosition; }

    private bool _isJump;
    public bool IsJump
    {
        get => _isJump;
    }
    private bool _isInventory = false;
    public bool IsInventory { get => _isInventory; }

    // private int ABS { get; set; }

    private int damage;
    public int criticalDamage
    {
        get { return criticalDamage * 2; }
    }

    public int nomalDamage
    {
        get;
        private set;
    }

    private PlayerControl _playerControl;
    private PlayerInput _playerInput;
    private PlayerAction _input;
    private Coroutine _onInputCoroutine;
    public Action interactionAction;

    private void OnValidate()
    {
        InIt();
    }

    private void InIt()
    {
        if (_playerControl == null) _playerControl = transform.GetComponentDebug<PlayerControl>();
        if (_playerInput == null) _playerInput = transform.GetComponentDebug<PlayerInput>();
    }

    private void Awake()
    {
        InIt();
    }

    private void OnEnable()
    {
        nomalDamage = 5;
        _input = new PlayerAction();
        _input.Player.Move.performed += OnMove;
        _input.Player.Move.canceled += StopMove;
        _input.Player.Jump.started += OnJump;
        _input.Player.Jump.canceled += StopJump;
        _input.Player.MousePosition.started += OnMousePosition;
        _input.Player.MousePosition.canceled += StopMousePosition;
        _input.Player.Interaction.started += OnInteraction;
        _input.Player.Inventory.started += OnInventory;

        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= OnMove;
        _input.Player.Move.canceled -= StopMove;
        _input.Player.Jump.started -= OnJump;
        _input.Player.Jump.canceled -= StopJump;
        _input.Player.MousePosition.started -= OnMousePosition;
        _input.Player.MousePosition.canceled -= StopMousePosition;
        _input.Player.Interaction.started -= OnInteraction;
        _input.Player.Inventory.started -= OnInventory;

        _input.Disable();
    }


    //플레이어의 움직임이 감지되었을 때 실행할 메서드
    private void OnMove(InputAction.CallbackContext context)
    {
        _playerMoveDir = context.ReadValue<Vector2>().normalized;

        if (_onInputCoroutine == null)
        {
            _onInputCoroutine = StartCoroutine(FixedUpdateOnInput());
        }
    }


    //플레이어의 움직임이 멈췄을 때 실행할 메서드
    private void StopMove(InputAction.CallbackContext context)
    {
        _playerMoveDir = Vector2.zero;
        _playerControl.MoveCharacter();
    }


    //플레이어가 점프 키를 입력했을 때 실행할 메서드
    private void OnJump(InputAction.CallbackContext context)
    {
        _isJump = true;
        if (_onInputCoroutine == null)
        {
            _onInputCoroutine = StartCoroutine(FixedUpdateOnInput());
        }
    }


    //플레이어가 점프 키를 땠을 때 실행할 메서드
    private void StopJump(InputAction.CallbackContext context)
    {
        _isJump = false;
    }


    //플레이어가 마우스를 움직였을 때 실행할 메서드
    private void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }


    //플레이어의 마우스가 멈췄을 때 실행할 메서드
    private void StopMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = Vector2.zero;
    }


    //플레이어가 상호작용 키를 입력했을 때 실행할 메서드
    private void OnInteraction(InputAction.CallbackContext context)
    {
        if (!(IsInventory)) interactionAction?.Invoke();
    }


    //플레이어가 인벤토리 키를 입력했을 때 실행할 메서드
    private void OnInventory(InputAction.CallbackContext context)
    {
        _isInventory = !_isInventory;
        UIManager.Instance.ActiveInventory(_isInventory);
    }

    //플레이어가 키를 입력했을 때 실행할 코루틴 
    //TODO: 코드 최적화 가능
    IEnumerator FixedUpdateOnInput()
    {
        while (IsJump || _playerMoveDir.magnitude > 0f)
        {
            if (IsInventory)
            {
                _playerMoveDir = Vector2.zero;
                _playerControl.MoveCharacter();
                _onInputCoroutine = null;
                yield break;
            }

            _playerControl.JumpCharacter();
            _playerControl.MoveCharacter();
            yield return new WaitForFixedUpdate();
        }
        _onInputCoroutine = null;
    }
}
