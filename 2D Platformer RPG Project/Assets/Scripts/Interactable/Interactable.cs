using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public UnityEvent interactAction;
    public GameObject pressE;

    private void Start()
    {
        pressE.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && isInRange)
        {
            interactAction.Invoke();
        }
        else
        {
            Debug.Log("Not in range of any interactable objects.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            pressE.SetActive(true);
            Debug.Log("Player is now in range.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            pressE.SetActive(false);
            Debug.Log("Player is no longer in range.");
        }
    }
}
