using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public float lerpSpeed;
    private Vector3 destinationPos;
    public Vector3 targetPos;
    public Transform targetObj;

    private void Update()
    {
        if (!targetObj)
        {
            destinationPos = player.transform.position + targetPos;
            transform.position = Vector3.Lerp(transform.position, destinationPos, lerpSpeed * Time.deltaTime);
        }
        else
        {
            destinationPos = targetObj.transform.position;
            transform.position = Vector3.Lerp(transform.position, destinationPos, lerpSpeed * Time.deltaTime);
        }
        transform.LookAt(player.transform);
        //(0, 7, -11);
    }

    public void setNewCameraPos(Transform newPos)
    {
        if (newPos)
        {
            newPos.gameObject.SetActive(true);
            if (targetObj)
                targetObj.gameObject.SetActive(false);
            if (newPos)
                targetObj = newPos;
        }
        else
        {
            targetObj = null;
        }
    }
}
