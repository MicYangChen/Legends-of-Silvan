using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquippedSlot : MonoBehaviour
{
    // SLOT APPEARANCE
    [SerializeField] private Image slotImage;
    [SerializeField] private TMP_Text slotName;

    // SLOT DATA
    [SerializeField] private ItemType itemType = new ItemType();

    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;

    // OTHER VARIABLES
    private bool slotInUse;

    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription)
    {
        // Update Image
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        // Update Data
        this.itemName = itemName;
        this.itemDescription = itemDescription;

        slotInUse = true;
    }


}
