using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaSystem : MonoBehaviour
{
    public int maxMana = 100;
    public float minRegenTime = 1.0f;
    public float punishingFactor = 2.0f;

    public Slider manaSlider; // Reference to the UI Mana Slider

    public int currentMana;
    private float regenTimer;
    private bool isRegenerating;

    public float acceleration = 10f;

    void Start()
    {
        currentMana = maxMana;
        regenTimer = 1.0f;
        isRegenerating = false;

        UpdateManaSlider();
    }

    void Update()
    {
        if (!isRegenerating && currentMana < maxMana)
        {
            regenTimer -= Time.deltaTime;

            if (regenTimer <= 0.0f)
            {
                currentMana++;
                regenTimer = CalculateRegenTime();
            }
        }
        else if (isRegenerating)
        {
            regenTimer -= Time.deltaTime;

            if (regenTimer <= 0.0f)
            {
                isRegenerating = false;
                regenTimer = minRegenTime; // Reset the timer to the base value after regeneration
            }
        }
        UpdateManaSlider();
    }

    public void UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;

            if (currentMana == 0)
            {
                regenTimer = punishingFactor * minRegenTime;
            }
            else
            {
                regenTimer = CalculateRegenTime();
            }

            isRegenerating = true;

            UpdateManaSlider();
        }
        else
        {
            Debug.Log("Not enough mana!");
        }
    }

    private float CalculateRegenTime()
    {
        // NOTE: NEEDS A FIX AT A LATER TIME
        float targetRegenTime = 2f;
        float accelerationRate = 1.0f / acceleration;

        float calculatedRegenTime = Mathf.Lerp(0.0f, targetRegenTime, 1.025f - Mathf.Exp(-accelerationRate * regenTimer));
        regenTimer = Mathf.Max(0.0f, regenTimer - Time.deltaTime);

        return calculatedRegenTime;
    }

    private void UpdateManaSlider()
    {
        if (manaSlider != null)
        {
            float manaPercentage = (float)currentMana / maxMana;

            // Hexadecimal 0068FF RGB values (0, 104, 255)
            Color startingColor = new Color(0, 104 / 255f, 255 / 255f);

            Color lerpedColor = Color.Lerp(startingColor, Color.red, 1.0f - manaPercentage);

            // Modify the slider's colors property
            var colors = manaSlider.colors;
            colors.normalColor = lerpedColor;
            manaSlider.colors = colors;

            // Set the slider value
            manaSlider.value = manaPercentage;
        }
    }
}
