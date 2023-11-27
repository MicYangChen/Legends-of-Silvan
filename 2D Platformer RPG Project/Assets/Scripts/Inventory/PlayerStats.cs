using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int strength, defense;

    [SerializeField] private TMP_Text strengthText, defenseText;

    void Start()
    {
        UpdateEquipmentStats();
    }

    public void UpdateEquipmentStats()
    {
        strengthText.text = strength.ToString();
        defenseText.text = defense.ToString();
    }
}
