using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMGR : MonoBehaviour
{
    private static AudioSource source;
    private static MusicMGR instance;
    private static AudioSource filterObject;

    [Header("Ambiance tracks")]
    public AudioClip[] ambiance;

    private void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        StartCoroutine(startNewAmbiance(0));
        filterObject = GameObject.FindGameObjectWithTag("filterObject").GetComponent<AudioSource>();
    }

    private IEnumerator startNewAmbiance(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        AudioClip clip = ambiance[Random.Range(0, ambiance.Length)];
        source.PlayOneShot(clip);
        StartCoroutine(startNewAmbiance(clip.length));
    }

    public static void playAudioClip(AudioClip clip, int Amount, int Timer, int timeAdded, bool UseFilter)
    {
        if (UseFilter)
        {
            if (Amount == 0)
            {
                filterObject.PlayOneShot(clip);
            }
            else
            {
                for (int i = 0; i < Amount; i++)
                {
                    instance.StartCoroutine(playAudioClips(clip, Timer, UseFilter));
                    Timer += timeAdded;
                }
            }
        }
        else
        {
            if (Amount == 0)
            {
                source.PlayOneShot(clip);
            }
            else
            {
                for (int i = 0; i < Amount; i++)
                {
                    instance.StartCoroutine(playAudioClips(clip, Timer, UseFilter));
                    Timer += timeAdded;
                }
            }
        }

    }

    public static IEnumerator playAudioClips(AudioClip clip, int time, bool useFilter)
    {
        yield return new WaitForSeconds(time);
        if (useFilter)
        {
            source.PlayOneShot(clip);
        }
        else
        {
            filterObject.PlayOneShot(clip);
        }
    }
}
