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
    public bool IsJump { get => _isJump; }
    private bool _isInventory = false;
    public bool IsInventory { get => _isInventory;}

    private PlayerControl _playerControl;
    private PlayerInput _playerInput;
    private PlayerAction _input;
    private Coroutine _onInputCoroutine;
    public Action interactionAction;

    private void OnValidate()
    {
        _playerControl = GetComponent<PlayerControl>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
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


    //�÷��̾��� �������� �����Ǿ��� �� ������ �޼���
    private void OnMove(InputAction.CallbackContext context)
    {
        _playerMoveDir = context.ReadValue<Vector2>().normalized;

        if(_onInputCoroutine == null)
        {
            _onInputCoroutine = StartCoroutine(FixedUpdateOnInput());
        }
        Debug.Log("�����̳���?");
    }


    //�÷��̾��� �������� ������ �� ������ �޼���
    private void StopMove(InputAction.CallbackContext context)
    {
        _playerMoveDir = Vector2.zero;
        _playerControl.MoveCharacter();
        Debug.Log("������?");
    }


    //�÷��̾ ���� Ű�� �Է����� �� ������ �޼���
    private void OnJump(InputAction.CallbackContext context)
    {
        _isJump = true;
        if (_onInputCoroutine == null)
        {
            _onInputCoroutine = StartCoroutine(FixedUpdateOnInput());
        }
        Debug.Log("�����߳���?");
    }


    //�÷��̾ ���� Ű�� ���� �� ������ �޼���
    private void StopJump(InputAction.CallbackContext context)
    {
        _isJump = false;
        Debug.Log("�����׸��ֿ�?");
    }


    //�÷��̾ ���콺�� �������� �� ������ �޼���
    private void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }


    //�÷��̾��� ���콺�� ������ �� ������ �޼���
    private void StopMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = Vector2.zero;
    }


    //�÷��̾ ��ȣ�ۿ� Ű�� �Է����� �� ������ �޼���
    private void OnInteraction(InputAction.CallbackContext context)
    {
        if(!(IsInventory))interactionAction?.Invoke();
    }


    //�÷��̾ �κ��丮 Ű�� �Է����� �� ������ �޼���
    private void OnInventory(InputAction.CallbackContext context)
    {
        _isInventory = !_isInventory;
        UIManager.Instance.ActiveInventory(_isInventory);
    }

    //�÷��̾ Ű�� �Է����� �� ������ �ڷ�ƾ
    //TODO: �ڵ� ����ȭ ����
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
