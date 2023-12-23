using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TreasureBoxController : MonoBehaviour
{
    public bool isOpen;

    public Animator animator;

    UIManager uiManager;
    InventoryManager inventoryManager;

    public GameObject TreasureBoxUI;

    private GameObject sceneBGMObject;
    private AudioSource sceneBGM;
    private float originalBGMVolume;
    public AudioSource boxOpenSound;

    public GameObject itemDrop;
    // TreasureBoxUI Description of item
    public Image ItemDescriptionImage;
    public TMP_Text ItemNameText;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        if (boxOpenSound == null)
        {
            Debug.LogError("AudioSoruce is not assigned to TreasureBoxController!");
        }

        sceneBGMObject = GameObject.Find("BGM");

        if (sceneBGMObject != null)
        {
            sceneBGM = sceneBGMObject.GetComponent<AudioSource>();

            if (sceneBGM == null)
            {
                Debug.LogError("AudioSource not found on the BGM GameObject");
            }
            else
            {
                originalBGMVolume = sceneBGM.volume;
            }
        }
        else
        {
            Debug.LogError("BGM GameObject not found.");
        }

        inventoryManager.canAccess = true;
    }

    public void OpenBox()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenBoxCoroutine());
        }
    }

    private IEnumerator OpenBoxCoroutine()
    {
        inventoryManager.canAccess = false;

        if (sceneBGM != null)
        {
            sceneBGM.volume = 0.02f;
        }
        
        Time.timeScale = 0;

        if (boxOpenSound != null)
        {
            boxOpenSound.Play();
        }

        Item itemComponent = itemDrop.GetComponent<Item>();

        UpdateUI(itemComponent.itemName, itemComponent.sprite);

        // Adds the item to player's inventory

        if (itemComponent != null)
        {
            bool foundEmptySlot = false;

            foreach (ItemSlot slot in inventoryManager.itemSlot)
            {
                if (!slot.isFull)
                {
                    foundEmptySlot = true;
                    break;
                }
            }

            bool foundEmptyEquipSlot = false;

            foreach (EquipmentSlot slot in inventoryManager.equipmentSlot)
            {
                if (!slot.isFull)
                {
                    foundEmptyEquipSlot = true;
                    break;
                }
            }

            if (foundEmptySlot && (itemComponent.itemType == ItemType.consumable || itemComponent.itemType == ItemType.collectible))
            {
                inventoryManager.AddItem(itemComponent.itemName, itemComponent.quantity, itemComponent.sprite, itemComponent.itemDescription, itemComponent.itemType);
            }
            else if (foundEmptyEquipSlot)
            {
                inventoryManager.AddItem(itemComponent.itemName, itemComponent.quantity, itemComponent.sprite, itemComponent.itemDescription, itemComponent.itemType);
            }
            else
            {
                Instantiate(itemDrop, GameObject.Find("Player").transform.position, Quaternion.identity);
            }
        }

        TreasureBoxUI.SetActive(true);
        isOpen = true;
        uiManager.openUI = true;
        Debug.Log("Treasure Box is now open.");
        animator.SetBool("isOpen", isOpen);

        // Wait for a specific amount of time

        yield return new WaitForSecondsRealtime(6.7f); // TreasureBox music lasts about 6.5 seconds.

        if (boxOpenSound != null)
        {
            boxOpenSound.Stop();
        }

        if (sceneBGM != null)
        {
            sceneBGM.volume = originalBGMVolume;
        }

        // Resume time
        Time.timeScale = 1;

        TreasureBoxUI.SetActive(false);
        uiManager.openUI = false;
        inventoryManager.canAccess = true;
    }

    private void UpdateUI(string itemName, Sprite itemSprite)
    {
        if (ItemNameText != null)
        {
            ItemNameText.text = "<color=#FFD700>" + itemName + "</color> has been obtained!";
        }
        if (ItemDescriptionImage != null)
        {
            ItemDescriptionImage.sprite = itemSprite;
        }
    }
}
