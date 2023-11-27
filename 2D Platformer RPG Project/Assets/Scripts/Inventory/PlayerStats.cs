using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private Damageable playerHP;

    public int health, strength, defense;

    [SerializeField] private TMP_Text healthText, strengthText, defenseText;

    void Start()
    {
        playerHP = GameObject.Find("Player").GetComponent<Damageable>();
        UpdateEquipmentStats();
    }

    public void UpdateEquipmentStats()
    {
        healthText.text = health.ToString();
        strengthText.text = strength.ToString();
        defenseText.text = defense.ToString();
    }
}
