using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public UnityEvent interactAction;

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
            Debug.Log("Player is now in range.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player is no longer in range.");
        }
    }
}
