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
    public Image npcImage;
    public Sprite npcSprite;
    public string npcName;
    public string[] dialogue;
    private int index;

    public GameObject continueButton;
    public float wordSpeed;
    public bool startDialogue;
    private bool isTyping;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>(); // If I later want to lock movement
    }

    void Update()
    {
        if (startDialogue && !isTyping)
        {
            npcDialogue.text = "";
            if (npcPanel.activeInHierarchy)
            {
                ResetText();
                startDialogue = false;
            }
            else
            {
                npcPanel.SetActive(true);
                npcNameText.text = npcName;
                UpdateNPCImage();
                StartCoroutine(Typing());
                startDialogue = false;
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
        UpdateNPCImage();
        npcPanel.SetActive(false);
        isTyping = false;
    }

    IEnumerator Typing()
    {
        isTyping = true;
        foreach (char letter in dialogue[index].ToCharArray())
        {
            npcDialogue.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        isTyping = false;
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

    private void UpdateNPCImage()
    {
        npcImage.sprite = npcSprite;
    }
}
