using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float lerpSpeed;
    private Vector3 destinationPos;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Transform targetObj;

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

    public void SetNewCameraPos(Transform newPos)
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
