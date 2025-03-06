using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private PlayerStatus _playerStatus;
    private PlayerInput _playerInput;
    private Rigidbody _rigidbody;

    private void OnValidate()
    {
        _playerStatus = GetComponent<PlayerStatus>();
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
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


    //플레이어가 땅에 닿고 있는지 확인하고 반환하는 메서드
    private bool isGround()
    {
        Ray[] ray = new Ray[]
        {
            new Ray(transform.position + new Vector3(-0.5f, 0.01f, -0.5f), Vector3.down),
            new Ray(transform.position + new Vector3(0.5f, 0.01f, -0.5f), Vector3.down),
            new Ray(transform.position + new Vector3(-0.5f, 0.01f, 0.5f), Vector3.down),
            new Ray(transform.position + new Vector3(0.5f, 0.01f, 0.5f), Vector3.down)
        };

        for(int i = 0; i < ray.Length; i++)
        {
            Debug.Log(Physics.Raycast(ray[i], 0.02f, LayerMask.GetMask("Ground")));
            Debug.DrawRay(transform.position, Vector2.down* 0.02f, Color.red);
            if (Physics.Raycast(ray[i], 0.02f, LayerMask.GetMask("Ground")))
            {
                return true;
            }
        }
        return false;
    }
}
