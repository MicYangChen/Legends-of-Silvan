using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health, strength, defense;
    public float critChance, ranged;
    public int currentExp, maxExp, currentLevel;

    [SerializeField] private TMP_Text levelText, healthText, attackText, strengthText, defenseText, critChanceText, rangedText;

    [SerializeField] private TMP_Text healthPreText, strengthPreText, defensePreText, critChancePreText, rangedPreText;
    [SerializeField] private GameObject healthNamePanel, strengthNamePanel, defenseNamePanel, critChanceNamePanel, rangedNamePanel;

    [SerializeField] private Image previewImage;

    [SerializeField] private GameObject selectedItemStats;
    [SerializeField] private GameObject selectedItemImage;

    void Start()
    {
        UpdateEquipmentStats();
    }

    private void Update()
    {
        Damageable playerHP = GameObject.Find("Player").GetComponent<Damageable>();
        healthText.text = playerHP.Health.ToString() + " / " + playerHP.MaxHealth.ToString();
        ExpSystem expSystem = GameObject.Find("Player").GetComponent<ExpSystem>();
        levelText.text = "Lv. " + expSystem.level.ToString();
    }

    public void UpdateEquipmentStats()
    {
        Damageable playerHP = GameObject.Find("Player").GetComponent<Damageable>();
        healthText.text = playerHP.Health.ToString() + " / " + playerHP.MaxHealth.ToString();
        attackText.text = (Mathf.RoundToInt(0.8f * strength)).ToString() + " - " + (Mathf.RoundToInt(1.2f * strength)).ToString();
        strengthText.text = strength.ToString();
        defenseText.text = defense.ToString();
        critChanceText.text = (Mathf.Min(100, critChance * 100)).ToString() + "%";
        rangedText.text = (ranged * 100).ToString() + "%";
    }

    public void PreviewEquipmentStats(int health, int strength, int defense, float critChance, float ranged, Sprite itemSprite)
    {
        if (health > 0)
        {
            healthPreText.text = health.ToString();
            healthNamePanel.SetActive(true);
        }
        else
        {
            healthPreText.text = "";
            healthNamePanel.SetActive(false);
        }
        if (strength > 0)
        {
            strengthPreText.text = strength.ToString();
            strengthNamePanel.SetActive(true);
        }
        else
        {
            strengthPreText.text = "";
            strengthNamePanel.SetActive(false);
        }

        if (defense > 0)
        {
            defensePreText.text = defense.ToString();
            defenseNamePanel.SetActive(true);
        }
        else
        {
            defensePreText.text = "";
            defenseNamePanel.SetActive(false);
        }

        if (critChance > 0)
        {
            critChancePreText.text = (Mathf.Min(100, critChance * 100)).ToString() + "%";
            critChanceNamePanel.SetActive(true);
        }
        else
        {
            critChancePreText.text = "";
            critChanceNamePanel.SetActive(false);
        }

        if (ranged > 0)
        {
            rangedPreText.text = (ranged * 100).ToString() + "%";
            rangedNamePanel.SetActive(true);
        }
        else
        {
            rangedPreText.text = "";
            rangedNamePanel.SetActive(false);
        }

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
