using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float fireResistance; 

    public int ModifyDamage(int damage)
    {
        // Apply resistance
        int modifiedDamage = Mathf.RoundToInt(damage * (1 - fireResistance));

        return modifiedDamage;
    }
}
