using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateOnEnter : MonoBehaviour
{
    [Header("Activate objects")]
    public bool activateObjects;
    public GameObject[] objectsToActivate;

    [Header("Deactivate objects")]
    public GameObject deactivateObjects;
    public GameObject[] objectsToDeactivate;

    [Header("Load scene options")]
    public bool loadScene;
    public int sceneIndex;

    [Header("Camera options")]
    public Transform newCameraPos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (activateObjects)
            {
                for (int i = 0; i < objectsToActivate.Length; i++)
                {
                    objectsToActivate[i].SetActive(true);
                }
                for (int i = 0; i < objectsToDeactivate.Length; i++)
                {
                    objectsToDeactivate[i].SetActive(false);
                }
            }

            if (newCameraPos)
            {
                Camera.main.GetComponent<CameraMovement>().setNewCameraPos(newCameraPos);
            }
            else
            {
                Camera.main.GetComponent<CameraMovement>().setNewCameraPos(null);
            }


            if (loadScene)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
            }
        }
    }


}
