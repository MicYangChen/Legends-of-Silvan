using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaSystem : MonoBehaviour
{
    PlayerArtifacts playerArtifacts;

    public int maxMana = 100;
    public float minRegenTime = 1.0f;
    public float punishingFactor = 2.0f;

    public Slider manaSlider; // Reference to the UI Mana Slider

    public int currentMana;
    private float regenTimer;
    private bool isRegenerating;

    public float acceleration = 10f;

    Color endingColor = Color.black;

    void Start()
    {
        playerArtifacts = GetComponent<PlayerArtifacts>();

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

            if (playerArtifacts.fireArtifactInUse)
            {
                // Hexadecimal Color FF3200
                Color startingColor = new Color(1f, 50f / 255f, 0);

                Color lerpedColor = Color.Lerp(startingColor, endingColor, 1.0f - manaPercentage);

                // Modify the slider's colors property
                var colors = manaSlider.colors;
                colors.normalColor = lerpedColor;
                manaSlider.colors = colors;
            }
            else if (playerArtifacts.windArtifactInUse)
            {
                // Hexadecimal Color 00FF20
                Color startingColor = new Color(0f, 1f, 32f / 255f);

                Color lerpedColor = Color.Lerp(startingColor, endingColor, 1.0f - manaPercentage);

                // Modify the slider's colors property
                var colors = manaSlider.colors;
                colors.normalColor = lerpedColor;
                manaSlider.colors = colors;
            }
            else if (playerArtifacts.electricArtifactInUse)
            {
                // Hexadecimal Color F5FF00
                Color startingColor = new Color(245f / 255f, 1f, 0);

                Color lerpedColor = Color.Lerp(startingColor, endingColor, 1.0f - manaPercentage);

                // Modify the slider's colors property
                var colors = manaSlider.colors;
                colors.normalColor = lerpedColor;
                manaSlider.colors = colors;
            }
            else
            {
                // Hexademical Color 32223C
                Color startingColor = new Color(50f / 255f, 34f / 255f, 60f / 255f);

                Color lerpedColor = Color.Lerp(startingColor, endingColor, 1.0f - manaPercentage);

                // Modify the slider's colors property
                var colors = manaSlider.colors;
                colors.normalColor = lerpedColor;
                manaSlider.colors = colors;
            }

            // Set the slider value
            manaSlider.value = manaPercentage;
        }
    }
}
