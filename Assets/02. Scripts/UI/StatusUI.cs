using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class StatusUI : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private Image _imgHealth;
    [ShowInInspector, ReadOnly]
    private Image _imgStamina;

    private void OnValidate()
    {
        InIt();
    }


    private void InIt()
    {
        if (_imgHealth == null) _imgHealth = "HealthValue".GetComponentNameDFS<Image>();
        if (_imgStamina == null) _imgStamina = "StaminaValue".GetComponentNameDFS<Image>();
    }


    private void Awake()
    {
        InIt();
    }


    public void UpdateStaminaAmount(float staminaFillAmount)
    {
        _imgStamina.fillAmount = staminaFillAmount;
    }

    public void UpdateHealthAmount(float healthFillAmount)
    {
        _imgHealth.fillAmount = healthFillAmount;
    }
}
