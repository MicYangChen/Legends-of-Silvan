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

    public void UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            GameObject.Find("Player").GetComponent<Damageable>().Heal(amountToChangeStat);
        }
        if (statToChange == StatToChange.attack_power)
        {
            Attack[] attackComponents = GameObject.Find("Player").GetComponentsInChildren<Attack>();
            foreach (Attack attackComponent in attackComponents)
            {
                attackComponent.ChangeAttackDamage(amountToChangeStat);
            }
        }
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
