using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float speed = 5f;
    private float jumpForce = 5f;
    private float maxHealth = 100f;
    private float curHealth = 100f;
    private float maxStamina = 100f;
    private float curStamina = 100f;
    private float maxPositionY;
    private bool isGround = false;

    //TODO: 추후에 해당 변수들 이동 필요
    private float sensitivity = 0.1f;
    private float maxCurXRot = 90;
    private float minCurXRot = -90f;
    private float consumptionJump = 20f;
    //

    public float Speed { get => speed; }
    public float JumpForce { get => jumpForce; }
    public float MaxHealth { get => maxHealth; }
    public float CurHealth { get => curHealth; }
    public float MaxStamina { get => maxStamina; }
    public float CurStamina { get => curStamina; }
    public bool IsGround { get => isGround; }

    //TODO: 추후에 해당 변수들 이동 필요
    public float Sensitivity { get => sensitivity; }
    public float MaxCurXRot { get => maxCurXRot; }
    public float MinCurXRot { get => minCurXRot; }
    //

    private PlayerControl _playerControl;

    private void OnValidate()
    {
        _playerControl = GetComponent<PlayerControl>();
    }

    private void Update()
    {
        //TODO: 플레이어의 스태미나가 점차 상승하는 메서드 필요.
        UIManager.Instance.statusUI.UpdateStaminaAmount(CurStamina / MaxStamina);
        if (CheckIsGround())
        {
            isGround = true;
            float temp = maxPositionY - transform.position.y - 3f;
            if (temp > 0)
            {
                HealthChange(-temp * 10f);
            }
            maxPositionY = transform.position.y;
        }
        else
        {
            isGround = false;
            maxPositionY = Mathf.Max(maxPositionY, transform.position.y);
        }
    }


    public void HealthChange(float value)
    {
        curHealth += value;
        curHealth = Mathf.Clamp(curHealth, 0f, MaxHealth);
        if (curHealth == 0f)
        {
            //TODO: 사망처리 필요
        }
    }


    public void StaminaChange(float value)
    {
        curStamina += value;
        curStamina = Mathf.Clamp(curStamina, 0f, MaxStamina);
    }


    //요구하는 스태미나가 사용 가능한지 확인하고 가능하다면 true를 반환하며 깎는 메서드
    public bool CheckAndUseStamina(float value)
    {
        if(curStamina >= value)
        {
            curStamina -= value;
            return true;
        }
        return false;
    }


    public bool CheckJumpStamina()
    {
        if (curStamina >= consumptionJump)
        {
            curStamina -= consumptionJump;
            return true;
        }
        return false;
    }


    //플레이어가 땅에 닿고 있는지 확인하고 반환하는 메서드
    public bool CheckIsGround()
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
}
