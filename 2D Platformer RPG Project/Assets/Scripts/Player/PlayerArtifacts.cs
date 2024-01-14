using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArtifacts : MonoBehaviour
{
    public bool FireArtifact { get; private set; }
    public List<string> FireArtifactItemNames = new List<string>
    {
        "Emberheart Amulet"
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

    private void Start()
    {
        // Check for Fire Artifact
        artifactFireSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/ArtifactFireSlot");
        artifactFireEquippedSlot = artifactFireSlotObject.GetComponent<EquippedSlot>();

        // Check for Wind Artifact
        artifactWindSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/ArtifactWindSlot");
        artifactWindEquippedSlot = artifactWindSlotObject.GetComponent<EquippedSlot>();

        // Check for Electric Artifact
        artifactElectricSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/ArtifactElectricSlot");
        artifactElectricEquippedSlot = artifactElectricSlotObject.GetComponent<EquippedSlot>();
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
}
