using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBoxController : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;
    UIManager uiManager;
    public GameObject TreasureBoxUI;
    private GameObject sceneBGMObject;
    private AudioSource sceneBGM;
    private float originalBGMVolume;
    public AudioSource boxOpenSound;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        if (boxOpenSound == null)
        {
            Debug.LogError("AudioSoruce is not assigned to TreasureBoxController!");
        }

        sceneBGMObject = GameObject.Find("BGM");

        if (sceneBGMObject != null)
        {
            sceneBGM = sceneBGMObject.GetComponent<AudioSource>();

            if (sceneBGM == null)
            {
                Debug.LogError("AudioSource not found on the BGM GameObject");
            }
            else
            {
                originalBGMVolume = sceneBGM.volume;
            }
        }
        else
        {
            Debug.LogError("BGM GameObject not found.");
        }
    }

    public void OpenBox()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenBoxCoroutine());
        }
    }

    private IEnumerator OpenBoxCoroutine()
    {
        if (sceneBGM != null)
        {
            sceneBGM.volume = 0.02f;
        }
        
        Time.timeScale = 0;

        if (boxOpenSound != null)
        {
            boxOpenSound.Play();
        }

        TreasureBoxUI.SetActive(true);
        isOpen = true;
        uiManager.openUI = true;
        Debug.Log("Treasure Box is now open.");
        animator.SetBool("isOpen", isOpen);

        // Wait for a specific amount of time

        yield return new WaitForSecondsRealtime(6.7f);

        if (boxOpenSound != null)
        {
            boxOpenSound.Stop();
        }

        if (sceneBGM != null)
        {
            sceneBGM.volume = originalBGMVolume;
        }

        // Resume time
        Time.timeScale = 1;

        TreasureBoxUI.SetActive(false);
        uiManager.openUI = false;
    }
}
