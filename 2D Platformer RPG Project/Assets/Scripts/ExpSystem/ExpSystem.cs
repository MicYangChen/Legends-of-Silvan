using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpSystem : MonoBehaviour
{
    public int level;
    public int exp;
    public int requireExp;

    public LevelConfig levelConfig;

    public Image expBar;
    public TMP_Text expText;
    public TMP_Text levelText;

    void Start()
    {
        CalculateRequiredExp();
    }

    public void CalculateRequiredExp()
    {
        requireExp = levelConfig.GetRequiredExp(level);
        UpdateUI();
    }

    public void IncreaseExp(int value)
    {
        exp += value;
        UpdateUI();

        if (exp >= requireExp)
        {
            // Incase multiple level ups:
            while (exp >= requireExp)
            {
                exp -= requireExp;
                LevelUp();
            }
        }
    }

    public void LevelUp()
    {
        if (level < levelConfig.MaxLevel)
        {
            level++;
            CalculateRequiredExp();
        }
        else
        {
            
        }
    }

    // Update is called once per frame
    void UpdateUI()
    {
        if (level < levelConfig.MaxLevel)
        {
            expBar.fillAmount = ((float)exp / (float)requireExp);
            levelText.text = "LVL: " + level;
            expText.text = exp + " / " + requireExp;
        }
        else
        {
            expBar.fillAmount = 1f;
            levelText.text = "LVL: " + levelConfig.MaxLevel + " (MAX)";
            expText.text = "0 / 0";
        }
    }
}
