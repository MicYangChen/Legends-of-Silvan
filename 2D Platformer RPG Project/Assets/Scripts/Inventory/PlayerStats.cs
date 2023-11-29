using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private Damageable playerHP;

    public int health, strength, defense;

    [SerializeField] private TMP_Text healthText, strengthText, defenseText;

    [SerializeField] private TMP_Text healthPreText, strengthPreText, defensePreText;

    [SerializeField] private Image previewImage;

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

    public void PreviewEquipmentStats(int health, int strength, int defense, Sprite itemSprite)
    {
        healthPreText.text = health.ToString();
        strengthPreText.text = strength.ToString();
        defensePreText.text = defense.ToString();

        previewImage.sprite = itemSprite;
    }
}
