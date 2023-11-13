using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated = false;

    public Canvas inventoryCanvas;

    // Start is called before the first frame update
    private void Awake()
    {
        inventoryCanvas = FindObjectOfType<Canvas>();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInventoryOpen(InputAction.CallbackContext context)
    {
        if (context.started && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
            Debug.Log("working??");
        }
        else if (context.started && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
            Debug.Log("working??");
        }
    }
}
