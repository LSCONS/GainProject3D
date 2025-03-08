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
        StartCoroutine(enumerator(5, 5));
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }


    private void LateUpdate()
    {
        Rotate();
    }


    //플레이어가 움직임.
    private void Move()
    {
        _rigidbody.velocity =
            transform.right * _playerInput.PlayerMoveDir.x * _playerStatus.Speed +
            transform.forward * _playerInput.PlayerMoveDir.y * _playerStatus.Speed +
            _rigidbody.velocity.y * Vector3.up;
    }


    //플레이어가 점프함.
    private void Jump()
    {
        if (_playerInput.IsJump && isGround())
        {
            _rigidbody.AddForce(Vector2.up * _playerStatus.JumpForce, ForceMode.Impulse);
        }
    }

    //플레이어를 회전시킴
    private void Rotate()
    {
        _curCameraXRot += _playerInput.MousePosition.y * _playerStatus.Sensitivity;
        _curCameraXRot = Mathf.Clamp(_curCameraXRot, _playerStatus.MinCurXRot, _playerStatus.MaxCurXRot);
        _camera.transform.localEulerAngles = -_curCameraXRot * Vector3.right;
        transform.eulerAngles += _playerInput.MousePosition.x * _playerStatus.Sensitivity * Vector3.up;
    }

    //플레이어가 땅에 닿고 있는지 확인하고 반환하는 메서드
    private bool isGround()
    {
        Ray[] ray = new Ray[]
        {
            new Ray(transform.position + Vector3.forward * 0.3f + Vector3.up * 0.01f, Vector3.down),
            new Ray(transform.position + Vector3.back * 0.3f+ Vector3.up * 0.01f, Vector3.down),
            new Ray(transform.position + Vector3.right * 0.3f+ Vector3.up * 0.01f, Vector3.down),
            new Ray(transform.position + Vector3.left * 0.3f+ Vector3.up * 0.01f, Vector3.down)
        };

        for (int i = 0; i < ray.Length; i++)
        {
            if (Physics.Raycast(ray[i], 0.02f, LayerMask.GetMask("Ground")))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator enumerator(int a, int b)
    {
        a = 5;
        b = 5;
        Debug.Log("스피드 늘려줘요");
        yield return new WaitForSeconds(5f); //나 1초 끝났어 이제 실행줘
        Debug.Log("스피드 끝났어요");
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector2.down * 2f, Color.red);
    }

    public void OnJumpPlatform(float jumpForce)
    {
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
    }
}
