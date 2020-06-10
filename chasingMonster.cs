using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingMonster : MonoBehaviour
{
    public float movementSpeed = 0.1f;
    private AudioSource source;

    [Header("Audio config")]
    public AudioClip awakenSound;
    public AudioClip[] randomSounds;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(awakenSound);
        StartCoroutine(playSound());
    }

    private void Update()
    {
        transform.Translate(transform.right * movementSpeed * Time.deltaTime);
    }

    private IEnumerator playSound()
    {
        yield return new WaitForSeconds(Random.Range(3, 10));
        source.PlayOneShot(randomSounds[Random.Range(0, randomSounds.Length)]);
        StartCoroutine(playSound());
    }
}
