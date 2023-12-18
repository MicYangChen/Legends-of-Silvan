using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health, strength, defense, ranged;
    public int currentExp, maxExp, currentLevel;

    [SerializeField] private TMP_Text healthText, strengthText, defenseText, rangedText;

    [SerializeField] private TMP_Text healthPreText, strengthPreText, defensePreText, rangedPreText;
    [SerializeField] private GameObject healthNamePanel, strengthNamePanel, defenseNamePanel, rangedNamePanel;

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
    }

    public void UpdateEquipmentStats()
    {
        Damageable playerHP = GameObject.Find("Player").GetComponent<Damageable>();
        healthText.text = playerHP.Health.ToString() + " / " + playerHP.MaxHealth.ToString();
        strengthText.text = strength.ToString();
        defenseText.text = defense.ToString();
        rangedText.text = ranged.ToString();
    }

    public void PreviewEquipmentStats(int health, int strength, int defense, int ranged, Sprite itemSprite)
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

        if (ranged > 0)
        {
            rangedPreText.text = ranged.ToString();
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
