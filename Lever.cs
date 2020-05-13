using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool activated;

    [Header("Object options")]
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDestroy;
    public Animator anim;
    public Light objectLight;

    [Header("Sound options")]
    public AudioClip soundToPlay;
    public int Amount = 0;
    public int Timer = 0;
    public int timeAdded = 0;
    public bool useFilter = true;

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
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }

            MusicMGR.playAudioClip(soundToPlay, Amount, Timer, timeAdded, useFilter);
        }
    }
}
