using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public GameObject levelUpTextPrefab;
    public GameObject critDamageTextPrefab;

    public Canvas gameCanvas;

    public bool openUI;

    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        // Text when character gets hit
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthReceived)
    {
        // Text when character gets healed
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthReceived.ToString();
    }

    public void CharacterLevelUp(GameObject character, string leveledUp)
    {
        // Text when character levels up
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(levelUpTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = leveledUp;
    }

    public void CharacterTookCritDamage(GameObject character, int critDamageReceived)
    {
        // Text when character gets hit
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(critDamageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = critDamageReceived.ToString();
    }

    // If UI Manager is enabled or not
    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
        CharacterEvents.characterLeveledUp += CharacterLevelUp;
        CharacterEvents.characterCritDamaged += CharacterTookCritDamage;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
        CharacterEvents.characterLeveledUp -= CharacterLevelUp;
        CharacterEvents.characterCritDamaged -= CharacterTookCritDamage;
    }

    private void Start()
    {
        // gameCanvas = FindObjectOfType<Canvas>();
        gameCanvas = GameObject.Find("GameCanvas").GetComponent<Canvas>();
    }
}
