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

    //TODO: ���Ŀ� �ش� ������ �̵� �ʿ�
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

    //TODO: ���Ŀ� �ش� ������ �̵� �ʿ�
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


    //ü���� ���� ������ �ִ� �޼���
    public void HealthChange(float value)
    {
        curHealth += value;
        curHealth = Mathf.Clamp(curHealth, 0f, MaxHealth);
        UIManager.Instance.statusUI.UpdateHealthAmount(CurHealth / MaxHealth);

        if (curHealth == 0f)
        {
            //TODO: ���ó�� �ʿ�
        }
    }


    //���¹̳��� ���� ������ �ִ� �޼���
    public void StaminaChange(float value)
    {
        curStamina += value;
        curStamina = Mathf.Clamp(curStamina, 0f, MaxStamina);
        UIManager.Instance.statusUI.UpdateStaminaAmount(CurStamina / MaxStamina);
    }


    //�䱸�ϴ� ���¹̳��� ��� �������� Ȯ���ϰ� �����ϴٸ� true�� ��ȯ�ϸ� ��� �޼���
    public bool CheckAndUseStamina(float value)
    {
        if(curStamina >= value)
        {
            curStamina -= value;
            return true;
        }
        return false;
    }


    //������ ������ ���¹̳����� Ȯ���ϰ� ���̸� bool���� ��ȯ�ϴ� �޼���
    public bool CheckJumpStamina()
    {
        if (curStamina >= consumptionJump)
        {
            curStamina -= consumptionJump;
            return true;
        }
        return false;
    }


    //�÷��̾ ���� ��� �ִ��� Ȯ���ϰ� ��ȯ�ϴ� �޼���
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


    //�ش� ���¹̳� ���� �ڷ�ƾ�� �����ϴ� �޼���
    public void StartStaminaCoroutine(float value, float time)
    {
        StartCoroutine(StaminaSpeedUp(value, time));
    }


    //���¹̳��� ä������ �ӵ��� �÷��ִ� �ڷ�ƾ
    private IEnumerator StaminaSpeedUp(float value, float time)
    {
        Debug.Log("������ ȿ�� ����");
        staminaRecoverySpeed += value;
        yield return new WaitForSeconds(time);
        staminaRecoverySpeed -= value;
    }
}
