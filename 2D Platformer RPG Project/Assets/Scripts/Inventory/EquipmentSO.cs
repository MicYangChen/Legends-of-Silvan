using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int health, strength, defense;

    public void EquipItem()
    {
        PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerstats.health += health;
        playerstats.strength += strength;
        playerstats.defense += defense;

        playerstats.UpdateEquipmentStats();
    }

    public void UnEquipItem()
    {
        PlayerStats playerstats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerstats.health -= health;
        playerstats.strength -= strength;
        playerstats.defense -= defense;

        playerstats.UpdateEquipmentStats();
    }
}
