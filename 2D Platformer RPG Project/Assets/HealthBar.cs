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
        if (player == null)
        {
            Debug.Log("No 'Player' found in the scene.");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }

    void Start()
    {
        hpSlider.value = CalcSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        hpBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHPChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHPChanged);
    }

    private float CalcSliderPercentage(float currentHP, float maxHP)
    {
        return currentHP / maxHP;
    }

    private void OnPlayerHPChanged(int newHP, int maxHP)
    {
        hpSlider.value = CalcSliderPercentage(newHP, maxHP);
        hpBarText.text = "HP " + newHP + " / " + maxHP;
    }
}
