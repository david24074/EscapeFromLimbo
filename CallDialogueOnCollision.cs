using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogueOnCollision : MonoBehaviour
{
    public string textToDisplay;
    public float fadeTimer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DisplayDialogue.activateText(textToDisplay, fadeTimer);
            Destroy(transform.gameObject);
        }
    }
}
