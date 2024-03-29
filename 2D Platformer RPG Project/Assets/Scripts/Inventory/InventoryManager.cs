using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class InventoryManager : MonoBehaviour
{
    public bool canAccess = true;

    UIManager uiManager;
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;
    public GameObject EquipmentDescriptionPanel;

    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;
    public EquippedSlot[] equippedSlot;

    public ItemSO[] itemSOs;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (context.started && InventoryMenu.activeSelf && canAccess)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            EquipmentDescriptionPanel.SetActive(false);
            uiManager.openUI = false;
            Debug.Log("UI Closed!");
        }
        else if (context.started && !InventoryMenu.activeSelf && canAccess)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            EquipmentMenu.SetActive(false);
            EquipmentDescriptionPanel.SetActive(false);
            uiManager.openUI = true;
            Debug.Log("UI Opened!");
        }
    }

    public void OnEquipmentOpen(InputAction.CallbackContext context)
    {
        if (context.started && EquipmentMenu.activeSelf && canAccess)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(false);
            EquipmentDescriptionPanel.SetActive(false);
            uiManager.openUI = false;
            Debug.Log("UI Closed!");
        }
        else if (context.started && !EquipmentMenu.activeSelf && canAccess)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(false);
            EquipmentMenu.SetActive(true);
            EquipmentDescriptionPanel.SetActive(false);
            uiManager.openUI = true;
            Debug.Log("UI Opened!");
        }
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        if (itemType == ItemType.consumable || itemType == ItemType.collectible)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);
                    }
                    return leftOverItems;
                }
            }
            return quantity;
        }
        else
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (equipmentSlot[i].isFull == false && equipmentSlot[i].itemName == itemName || equipmentSlot[i].quantity == 0)
                {
                    int leftOverItems = equipmentSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription, itemType);
                    }
                    return leftOverItems;
                }
            }
            return quantity;
        }
    }

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            if (itemSOs[i].itemName == itemName)
            {
                bool useable = itemSOs[i].UseItem();
                return useable;
            }
        }
        return false;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            equipmentSlot[i].selectedShader.SetActive(false);
            equipmentSlot[i].thisItemSelected = false;
        }
        for (int i = 0; i < equippedSlot.Length; i++)
        {
            equippedSlot[i].selectedShader.SetActive(false);
            equippedSlot[i].thisItemSelected = false;
        }
    }
}

// Item type to know if item goes to equipment inventory or normal inventory and know what equipment slot an equipment belongs to.
public enum ItemType
{
    consumable,
    collectible,
    helmet,
    armor,
    boots,
    subWeapon,
    weapon,
    accessory,
    artifactFire,
    artifactWind,
    artifactElectric,
    none,
};