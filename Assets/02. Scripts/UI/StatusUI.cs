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
        _imgHealth = transform.GetGameObjectSameNameDFS("HealthValue").GetComponent<Image>();
        _imgStamina = transform.GetGameObjectSameNameDFS("StaminaValue").GetComponent<Image>();
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
