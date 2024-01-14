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

    /* public bool WindArtifact { get; private set; }
    public List<string> WindArtifactItemNames = new List<string>
    {
        "NOT YET DECIDED"
    }; */

    /* public bool ElectricArtifact { get; private set; }
    public List<string> ElectricArtifactItemNames = new List<string>
    {
        "NOT YET DECIDED"
    }; */

    public GameObject artifactSlotObject;
    private EquippedSlot artifactFireEquippedSlot;

    private void Start()
    {
        // Check for Fire Artifact
        artifactSlotObject = GameObject.Find("----------UI----------/InventoryCanvas/EquipmentMenu/PlayerEquipmentPanel/PlayerEquipmentPanel/RightPanel/ArtifactFireSlot");
        artifactFireEquippedSlot = artifactSlotObject.GetComponent<EquippedSlot>();
    }

    // Fire Artifact
    public void CheckFireArtifact()
    {
        FireArtifact = FireArtifactItemNames.Contains(artifactFireEquippedSlot.itemName);
    }
}
