using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemSO;

[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;

    // Stats
    public int health, strength, defense;

    // Multipliers or Scaling
    public float critChance, ranged, mpCost;

    [SerializeField] private Sprite itemSprite;

    public void PreviewEquipment()
    {
        GameObject.Find("StatManager").GetComponent<PlayerStats>().
            PreviewEquipmentStats(health, strength, defense, critChance, ranged, mpCost, itemSprite);
    }

    public void EquipItem()
    {
        PlayerStats playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        Damageable playerHP = GameObject.Find("Player").GetComponent<Damageable>();
        playerHP.IncreaseMaxHealth(health);
        playerStats.health += health;
        playerStats.strength += strength;
        playerStats.defense += defense;
        playerStats.critChance += critChance;
        playerStats.ranged += ranged;

        playerStats.UpdateEquipmentStats();
    }

    public void UnEquipItem()
    {
        PlayerStats playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        Damageable playerHP = GameObject.Find("Player").GetComponent<Damageable>();
        playerHP.DecreaseMaxHealth(health);
        if (playerHP.MaxHealth < playerHP.Health)
        {
            playerHP.Health = playerHP.MaxHealth;
        }
        playerStats.health -= health;
        playerStats.strength -= strength;
        playerStats.defense -= defense;
        playerStats.critChance -= critChance;
        playerStats.ranged -= ranged;

        playerStats.UpdateEquipmentStats();
    }
}
