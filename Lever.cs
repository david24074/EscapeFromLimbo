using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool activated;

    [Header("Object options")]
    public GameObject[] objectsToActivate;
    public Animator anim;

    [Header("Sound options")]
    public AudioClip soundToPlay;
    public int Amount = 0;
    public int Timer = 0;
    public int timeAdded = 0;

    public void activateLever()
    {
        if (!activated)
        {
            activated = true;
            anim.Play("Activate");
            foreach(GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
            MusicMGR.playAudioClip(soundToPlay, Amount, Timer, timeAdded);
        }
    }
}
