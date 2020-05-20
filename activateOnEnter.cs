using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateOnEnter : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDeactivate;
    public Transform newCameraPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                objectsToActivate[i].SetActive(true);
            }
            for (int i = 0; i < objectsToDeactivate.Length; i++)
            {
                objectsToDeactivate[i].SetActive(false);
            }

            if (newCameraPos)
            {
                Camera.main.GetComponent<CameraMovement>().setNewCameraPos(newCameraPos);
            }
            else
            {
                Camera.main.GetComponent<CameraMovement>().setNewCameraPos(null);
            }
        }
    }


}
