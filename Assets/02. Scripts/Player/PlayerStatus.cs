using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using VInspector;

public class PlayerStatus : MonoBehaviour
{
    private float speed = 5f;
    private float jumpForce = 5f;
    private float maxHealth = 100f;
    private float curHealth = 100f;
    private float maxStamina = 100f;
    private float curStamina = 100f;
    private float maxPositionY;
    private float staminaRecoverySpeed = 20f;
    private bool isGround = false;

    private LayerMask _excludeLayerMask;

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
        _excludeLayerMask = ~(ReadonlyData.jumpPlatformLayerMask);
    }

    private void Update()
    {
        if (CheckIsGround())
        {
            isGround = true;
            float temp = maxPositionY - transform.position.y - 3f;
            if (temp > 0)
            {
                HealthChange(-temp * 10f);
            }
            maxPositionY = transform.position.y;
            StaminaChange(Time.deltaTime * staminaRecoverySpeed);
        }
        else
        {
            isGround = false;
            maxPositionY = Mathf.Max(maxPositionY, transform.position.y);
        }
    }


    //체력의 값에 변동을 주는 메서드
    public void HealthChange(float value)
    {
        curHealth += value;
        curHealth = Mathf.Clamp(curHealth, 0f, MaxHealth);
        UIManager.Instance.statusUI.UpdateHealthAmount(CurHealth / MaxHealth);

        if (curHealth == 0f)
        {
            //TODO: 사망처리 필요
        }
    }


    //스태미나의 값에 변동을 주는 메서드
    public void StaminaChange(float value)
    {
        curStamina += value;
        curStamina = Mathf.Clamp(curStamina, 0f, MaxStamina);
        UIManager.Instance.statusUI.UpdateStaminaAmount(CurStamina / MaxStamina);
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


    //점프가 가능한 스태미나인지 확인하고 줄이며 bool값을 반환하는 메서드
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
            if (Physics.Raycast(ray[i], 0.02f, _excludeLayerMask))
            {
                return true;
            }
        }
        return false;
    }


    //해당 스태미나 관련 코루틴을 실행하는 메서드
    public void StartStaminaCoroutine(float value, float time)
    {
        StartCoroutine(StaminaSpeedUp(value, time));
    }


    //스태미나가 채워지는 속도를 올려주는 코루틴
    private IEnumerator StaminaSpeedUp(float value, float time)
    {
        Debug.Log("아이템 효과 시작");
        staminaRecoverySpeed += value;
        yield return new WaitForSeconds(time);
        staminaRecoverySpeed -= value;
    }
}
