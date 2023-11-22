using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;

    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            Damageable playerHP = GameObject.Find("Player").GetComponent<Damageable>();
            if (playerHP.Health == playerHP.MaxHealth)
            {
                return false;
            }
            else
            {
                playerHP.Heal(amountToChangeStat);
                return true;
            }
        }
        if (statToChange == StatToChange.attack_power)
        {
            Attack[] attackComponents = GameObject.Find("Player").GetComponentsInChildren<Attack>();
            foreach (Attack attackComponent in attackComponents)
            {
                attackComponent.ChangeAttackDamage(amountToChangeStat);
            }
            return true;
        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health,
        attack_power
    };

    public enum AttributeToChange
    {
        none,
        strength,
        defense
    };
}
