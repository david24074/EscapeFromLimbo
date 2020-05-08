using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMGR : MonoBehaviour
{
    private static AudioSource source;
    private static MusicMGR instance;

    [Header("Ambiance tracks")]
    public AudioClip[] ambiance;

    private void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        StartCoroutine(startNewAmbiance(0));
    }

    private IEnumerator startNewAmbiance(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        AudioClip clip = ambiance[Random.Range(0, ambiance.Length)];
        source.PlayOneShot(clip);
        StartCoroutine(startNewAmbiance(clip.length));
    }

    public static void playAudioClip(AudioClip clip, int Amount, int Timer, int timeAdded)
    {
        if(Amount == 0)
        {
            source.PlayOneShot(clip);
        }
        else
        {
            for(int i = 0; i < Amount; i++)
            {
                instance.StartCoroutine(playAudioClips(clip, Timer));
                Timer += timeAdded;
            }
        }
    }

    public static IEnumerator playAudioClips(AudioClip clip, int timer)
    {
        yield return new WaitForSeconds(timer);
        source.PlayOneShot(clip);
    }
}
