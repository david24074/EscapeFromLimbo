using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayDialogue : MonoBehaviour
{
    public static TextMeshProUGUI dialogueText;
    public static Image dialogueBox;
    private static DisplayDialogue dialogue;

    private void Awake()
    {
        dialogueBox = GameObject.Find("DialogueBox").GetComponent<Image>();
        dialogue = this;
        dialogueText = GetComponent<TextMeshProUGUI>();
    }

    public static void activateText(string t, float timer)
    {
        dialogueText.text = t;
        dialogueBox.enabled = true;
        dialogue.StartCoroutine(stopText(timer));
    }

    public static IEnumerator stopText(float timer)
    {
        yield return new WaitForSeconds(timer);
        if (dialogueText)
            dialogueText.text = "";
        if (dialogueBox)
            dialogueBox.enabled = false;
    }
}
