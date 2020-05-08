using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float lerpSpeed;
    private Vector3 destinationPos;
    public Vector3 targetPos;

    private void Update()
    {
        destinationPos = player.transform.position + targetPos;
        transform.position = Vector3.Lerp(transform.position, destinationPos, lerpSpeed * Time.deltaTime);
        //(0, 7, -11);
    }
}
