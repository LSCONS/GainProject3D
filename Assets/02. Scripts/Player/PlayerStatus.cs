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

    //TODO: 추후에 해당 변수들 이동 필요
    private float sensitivity = 0.1f;
    private float maxCurXRot = 90;
    private float minCurXRot = -90f;
    //

    public float Speed { get => speed; }
    public float JumpForce { get => jumpForce; }
    public float MaxHealth { get => maxHealth; }
    public float CurHealth { get => curHealth; }

    //TODO: 추후에 해당 변수들 이동 필요
    public float Sensitivity { get => sensitivity; }
    public float MaxCurXRot { get => maxCurXRot; }
    public float MinCurXRot { get => minCurXRot; }
    //
}
