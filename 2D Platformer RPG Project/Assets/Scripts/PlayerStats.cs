using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health, strength, defense;
    public int currentExp, maxExp, currentLevel;

    [SerializeField] private TMP_Text healthText, strengthText, defenseText;

    [SerializeField] private TMP_Text healthPreText, strengthPreText, defensePreText;

    [SerializeField] private Image previewImage;

    [SerializeField] private GameObject selectedItemStats;
    [SerializeField] private GameObject selectedItemImage;

    void Start()
    {
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

        selectedItemImage.SetActive(true);
        selectedItemStats.SetActive(true);
    }

    public void TurnOffPreviewStats()
    {
        selectedItemImage.SetActive(false);
        selectedItemStats.SetActive(false);
    }
}
