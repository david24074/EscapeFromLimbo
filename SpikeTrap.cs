using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private Collider col;
    [SerializeField] private GameObject spikes;

    private void Start()
    {
        col = GetComponent<Collider>();
        StartCoroutine(enableObject());
    }

    private IEnumerator enableObject()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        col.enabled = true;
        spikes.SetActive(true);
        yield return new WaitForSeconds(1);
        col.enabled = false;
        spikes.SetActive(false);
        StartCoroutine(enableObject());
    }
}
