using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Damageable playerDamageable;
    public Slider hpSlider;
    public TMP_Text hpBarText;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }

    void Start()
    {
        hpSlider.value = CalcSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        hpBarText.text = "HP: " + Mathf.Max(0, playerDamageable.Health) + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHPChanged);
        playerDamageable.maxHealthChanged.AddListener(OnPlayerMaxHPChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHPChanged);
        playerDamageable.maxHealthChanged.RemoveListener(OnPlayerMaxHPChanged);
    }

    private float CalcSliderPercentage(float currentHP, float maxHP)
    {
        return currentHP / maxHP;
    }

    private void OnPlayerHPChanged(int newHP, int maxHP)
    {
        hpSlider.value = CalcSliderPercentage(newHP, maxHP);
        hpBarText.text = "HP: " + newHP + " / " + maxHP;
    }

    private void OnPlayerMaxHPChanged(int HP, int newMaxHP)
    {
        hpSlider.value = CalcSliderPercentage(HP, newMaxHP);
        hpBarText.text = "HP: " + HP + " / " + newMaxHP;
    }
}
