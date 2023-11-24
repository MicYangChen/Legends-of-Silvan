using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

// EquipmentSlot communicates with InventoryManager and EquippedSlot
public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    private InventoryManager inventoryManager;

    public GameObject selectedShader;
    public bool thisItemSelected;

    // ITEM DATA
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public ItemType itemType;

    // ITEM SLOT
    [SerializeField] private Image itemImage;

    // EQUIPPED SLOTS
    [SerializeField] private EquippedSlot helmetSlot, armorSlot, glovesSlot, bootsSlot, weaponSlot, accessorySlot, artifactSlot;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        // Check if slot already full
        if (isFull)
        {
            return quantity;
        }

        // Update Item Type
        this.itemType = itemType;

        // Update Name, Image & Description
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;

        // Update Quantity
        this.quantity = 1;
        isFull = true;

        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if (thisItemSelected)
        {
            EquipGear();
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    private void EquipGear()
    {
        switch (itemType)
        {
            case ItemType.helmet:
                {
                    helmetSlot.EquipGear(itemSprite, itemName, itemDescription);
                    break;
                }
            case ItemType.armor:
                {
                    armorSlot.EquipGear(itemSprite, itemName, itemDescription);
                    break;
                }
            case ItemType.gloves:
                {
                    glovesSlot.EquipGear(itemSprite, itemName, itemDescription);
                    break;
                }
            case ItemType.boots:
                {
                    bootsSlot.EquipGear(itemSprite, itemName, itemDescription);
                    break;
                }
            case ItemType.weapon:
                {
                    weaponSlot.EquipGear(itemSprite, itemName, itemDescription);
                    break;
                }
            case ItemType.accessory:
                {
                    accessorySlot.EquipGear(itemSprite, itemName, itemDescription);
                    break;
                }
            case ItemType.artifact:
                {
                    artifactSlot.EquipGear(itemSprite, itemName, itemDescription);
                    break;
                }
        }
        EmptySlot();
    }

    private void EmptySlot()
    {
        itemImage.sprite = emptySprite;
    }

    public void OnRightClick()
    {
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1;
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;

        // Create and modify the Sprite Renderer
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Ground";

        // Add a collider and set the location
        itemToDrop.AddComponent<BoxCollider2D>();
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1f, 0, 0);
        // If there is a need to change the size of the dropped item
        itemToDrop.transform.localScale = new Vector3(1f, 1f, 1f);

        // Remove the item
        this.quantity -= 1;
        if (this.quantity <= 0)
        {
            EmptySlot();
        }
    }

}
