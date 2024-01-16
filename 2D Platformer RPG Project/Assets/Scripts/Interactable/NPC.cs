using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    private UIManager uiManager;

    public GameObject npcPanel;
    public TMP_Text npcNameText;
    public TMP_Text npcDialogue;
    public string npcName;
    public string[] dialogue;
    private int index;

    public GameObject continueButton;
    public float wordSpeed;
    public bool startDialogue;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>(); // If I later want to lock movement
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && startDialogue)
        {
            npcDialogue.text = "";
            if (npcPanel.activeInHierarchy)
            {
                ResetText();
            }
            else
            {
                npcPanel.SetActive(true);
                npcNameText.text = npcName;
                StartCoroutine(Typing());
            }
        }

        if (npcDialogue.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NPCStartDialogue()
    {
        startDialogue = true;
    }

    public void ResetText()
    {
        npcDialogue.text = "";
        npcNameText.text = "";
        index = 0;
        npcPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            npcDialogue.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        continueButton.SetActive(false);

        if (index < dialogue.Length -1)
        {
            index++;
            npcDialogue.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ResetText();
            startDialogue = false;
        }
    }
}
