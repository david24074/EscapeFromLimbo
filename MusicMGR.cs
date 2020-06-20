using System.Collections;
using UnityEngine;

public class MusicMGR : MonoBehaviour
{
    private static AudioSource source;
    private static MusicMGR musicInstance;
    private static AudioSource filterObject;

    [Header("Ambiance tracks")]
    [SerializeField] private AudioClip[] ambiance;

    [SerializeField] private static GameObject instance;

    private void Start()
    {
        musicInstance = this;
        DontDestroyOnLoad(transform.gameObject);
        if (instance == null)
        {
            instance = transform.gameObject;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(StartNewAmbiance(0));
    }

    public static void FindFilter()
    {
        if (GameObject.FindGameObjectWithTag("filterObject"))
        filterObject = GameObject.FindGameObjectWithTag("filterObject").GetComponent<AudioSource>();
        source = GameObject.FindGameObjectWithTag("MusicMGR").GetComponent<AudioSource>();
    }

    private IEnumerator StartNewAmbiance(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        AudioClip clip = ambiance[Random.Range(0, ambiance.Length)];
        source.PlayOneShot(clip);
        StartCoroutine(StartNewAmbiance(clip.length));
    }

    public static void PlayAudioClip(AudioClip clip, int Amount, int Timer, int timeAdded, bool UseFilter)
    {
        if (!source)
            FindFilter();

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
                    musicInstance.StartCoroutine(PlayAudioClips(clip, Timer, UseFilter));
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
                    musicInstance.StartCoroutine(PlayAudioClips(clip, Timer, UseFilter));
                    Timer += timeAdded;
                }
            }
        }

    }

    public static IEnumerator PlayAudioClips(AudioClip clip, int time, bool useFilter)
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
