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


    //�÷��̾ ������.
    private void Move()
    {
        _rigidbody.velocity = 
            transform.right * _playerInput.PlayerMoveDir.x * _playerStatus.Speed + 
            transform.forward * _playerInput.PlayerMoveDir.y * _playerStatus.Speed +
            _rigidbody.velocity.y * Vector3.up;
    }

    
    //�÷��̾ ������.
    private void Jump()
    {
        if (_playerInput.IsJump && isGround())
        {
            _rigidbody.AddForce(Vector2.up * _playerStatus.JumpForce, ForceMode.Impulse);
        }
    }


    //�÷��̾ ���� ��� �ִ��� Ȯ���ϰ� ��ȯ�ϴ� �޼���
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
