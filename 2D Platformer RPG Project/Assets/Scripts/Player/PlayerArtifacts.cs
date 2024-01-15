using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerArtifacts : MonoBehaviour
{
    UIManager uiManager;
    public bool FireArtifact { get; private set; }
    public List<string> FireArtifactItemNames = new List<string>
    {
        "Emberheart Staff"
    };

    public bool WindArtifact { get; private set; }
    public List<string> WindArtifactItemNames = new List<string>
    {
        "Cyclonic Crest"
    };

    public bool ElectricArtifact { get; private set; }
    public List<string> ElectricArtifactItemNames = new List<string>
    {
        "Thunderguard Aegis"
    };

    public GameObject artifactFireSlotObject;
    private EquippedSlot artifactFireEquippedSlot;

    public GameObject artifactWindSlotObject;
    private EquippedSlot artifactWindEquippedSlot;

    public GameObject artifactElectricSlotObject;
    private EquippedSlot artifactElectricEquippedSlot;

    public GameObject elementColor;
    public RawImage elementColorToChange;
    Color endingColor = Color.black;

    public bool fireArtifactInUse = false;
    public bool windArtifactInUse = false;
    public bool electricArtifactInUse = false;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        // Check for Fire Artifact
        artifactFireSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/ArtifactFireSlot");
        artifactFireEquippedSlot = artifactFireSlotObject.GetComponent<EquippedSlot>();

        // Check for Wind Artifact
        artifactWindSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/ArtifactWindSlot");
        artifactWindEquippedSlot = artifactWindSlotObject.GetComponent<EquippedSlot>();

        // Check for Electric Artifact
        artifactElectricSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/ArtifactElectricSlot");
        artifactElectricEquippedSlot = artifactElectricSlotObject.GetComponent<EquippedSlot>();

        elementColor = GameObject.Find("----------UI----------/GameCanvas/PlayerUI/PlayerHPMPExpUI/ElementColor");
        elementColorToChange = elementColor.GetComponent<RawImage>();
    }

    // Fire Artifact
    public void CheckFireArtifact()
    {
        FireArtifact = FireArtifactItemNames.Contains(artifactFireEquippedSlot.itemName);
    }

    // Wind Artifact
    public void CheckWindArtifact()
    {
        WindArtifact = WindArtifactItemNames.Contains(artifactWindEquippedSlot.itemName);
    }

    // Electric Artifact
    public void CheckElectricArtifact()
    {
        ElectricArtifact = ElectricArtifactItemNames.Contains(artifactElectricEquippedSlot.itemName);
    }

    public void SwitchToFireArtifact(InputAction.CallbackContext context)
    {
        if (context.started && !uiManager.openUI)
        {
            CheckFireArtifact();
            if (FireArtifact)
            {
                fireArtifactInUse = true;
                windArtifactInUse = false;
                electricArtifactInUse = false;
                Debug.Log("Switched to Fire Artifact");
                // Hexadecimal Color FF3200
                var fireColor = new Color(1f, 50f / 255f, 0);
                var lerpedColor = Color.Lerp(fireColor, endingColor, 0.05f);
                elementColorToChange.color = lerpedColor;
            }
            else
            {
                Debug.Log("No Fire Artifact Equipped");
            }
        }
    }

    // Switch to Wind Artifact
    public void SwitchToWindArtifact(InputAction.CallbackContext context)
    {
        if (context.started && !uiManager.openUI)
        {
            CheckWindArtifact();
            if (WindArtifact)
            {
                fireArtifactInUse = false;
                windArtifactInUse = true;
                electricArtifactInUse = false;
                Debug.Log("Switched to Wind Artifact");
                // Hexadecimal Color 00FF20
                var windColor = new Color(0f, 1f, 32f / 255f);
                var lerpedColor = Color.Lerp(windColor, endingColor, 0.05f);
                elementColorToChange.color = lerpedColor;

            }
            else
            {
                Debug.Log("No Wind Artifact Equipped");
            }
        }
    }

    // Switch to Electric Artifact
    public void SwitchToElectricArtifact(InputAction.CallbackContext context)
    {
        if (context.started && !uiManager.openUI)
        {
            CheckElectricArtifact();
            if (ElectricArtifact)
            {
                fireArtifactInUse = false;
                windArtifactInUse = false;
                electricArtifactInUse = true;
                Debug.Log("Switched to Electric Artifact");
                // Hexadecimal Color F5FF00
                var electricColor = new Color(245f / 255f, 1f, 0);
                var lerpedColor = Color.Lerp(electricColor, endingColor, 0.05f);
                elementColorToChange.color = lerpedColor;
            }
            else
            {
                Debug.Log("No Electric Artifact Equipped");
            }
        }
    }
}
