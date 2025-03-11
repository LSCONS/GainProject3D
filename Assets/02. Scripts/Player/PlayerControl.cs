using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using VInspector;

public class PlayerControl : MonoBehaviour, IJumpPlatFormInteraction
{
    private PlayerStatus _playerStatus;
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;
    private Camera _camera;
    public float _curCameraXRot;
    public GameObject game;

    private void OnValidate()
    {
        _camera = Camera.main;
        _playerStatus = GetComponent<PlayerStatus>();
        _playerInput = GetComponent<PlayerInput>() ;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }




    private void LateUpdate()
    {
        if(!(_playerInput.IsInventory)) RotateCharacter();
    }



    //플레이어가 움직임.
    public void MoveCharacter()
    {
        _rigidbody.velocity =
            transform.right * _playerInput.PlayerMoveDir.x * _playerStatus.Speed +
            transform.forward * _playerInput.PlayerMoveDir.y * _playerStatus.Speed +
            _rigidbody.velocity.y * Vector3.up;
    }


    //플레이어가 점프함.
    public void JumpCharacter()
    {
        if (_playerInput.IsJump &&
            Mathf.Approximately(_rigidbody.velocity.y, 0) &&
            _playerStatus.IsGround &&
            _playerStatus.CheckJumpStamina())
        {
            _rigidbody.AddForce(Vector2.up * _playerStatus.JumpForce, ForceMode.Impulse);
        }
    }


    //플레이어를 회전시킴
    private void RotateCharacter()
    {
        _curCameraXRot += _playerInput.MousePosition.y * _playerStatus.Sensitivity;
        _curCameraXRot = Mathf.Clamp(_curCameraXRot, _playerStatus.MinCurXRot, _playerStatus.MaxCurXRot);
        _camera.transform.localEulerAngles = -_curCameraXRot * Vector3.right;
        transform.eulerAngles += _playerInput.MousePosition.x * _playerStatus.Sensitivity * Vector3.up;
    }


    //점프 플랫폼에 닿았을 때 플레이어를 특정 힘으로 밀어주는 메서드 
    public void OnJumpPlatform(float jumpForce)
    {
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
    }
}
