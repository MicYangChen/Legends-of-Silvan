using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxController : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;

    public void OpenBox()
    {
        if (!isOpen)
        {
            isOpen = true;
            Debug.Log("Treasure Box is now open.");
            animator.SetBool("isOpen", isOpen);
        }
    }
}
