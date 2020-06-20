using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 0.1f;
    private AudioSource source;

    [Header("Audio config")]
    [SerializeField] private AudioClip awakenSound;
    [SerializeField] private AudioClip[] randomSounds;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(awakenSound);
        StartCoroutine(PlaySound());
    }

    private void Update()
    {
        transform.Translate(transform.right * movementSpeed * Time.deltaTime);
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(3, 10));
        source.PlayOneShot(randomSounds[Random.Range(0, randomSounds.Length)]);
        StartCoroutine(PlaySound());
    }
}
