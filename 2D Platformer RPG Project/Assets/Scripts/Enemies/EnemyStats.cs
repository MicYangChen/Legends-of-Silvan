using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float fireResistance;
    public float windResistance;
    public float electricResistance;

    public int ModifyDamage(int damage, DamageType damageType)
    {
        float resistance = 0f;

        switch (damageType)
        {
            case DamageType.Fire:
                resistance = fireResistance;
                break;
            case DamageType.Wind:
                resistance = windResistance;
                break;
            case DamageType.Electric:
                resistance = electricResistance;
                break;
        }
        int modifiedDamage = Mathf.RoundToInt(damage * (1 - resistance));

        return modifiedDamage;
    }

    public enum DamageType
    {
        Fire,
        Wind,
        Electric,
    }
}
