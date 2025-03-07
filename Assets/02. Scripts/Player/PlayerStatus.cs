using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float speed = 5f;
    private float jumpForce = 10f;
    private float maxHealth = 100f;
    private float curHealth = 100f;
    private float sensitivity = 0.1f;

    public float Speed { get => speed; }
    public float JumpForce { get => jumpForce; }
    public float MaxHealth { get => maxHealth; }
    public float CurHealth { get => curHealth; }
    public float Sensitivity { get => sensitivity; }
}
