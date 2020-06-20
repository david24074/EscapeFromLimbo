using UnityEngine;

public class CallDialogueOnCollision : MonoBehaviour
{
    [SerializeField] private string textToDisplay;
    [SerializeField] private float fadeTimer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            DisplayDialogue.ActivateText(textToDisplay, fadeTimer);
            Destroy(this);
        }
    }
}
