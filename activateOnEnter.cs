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

    [Header("Spawn enemy options")]
    public int amountEnemies;
    public GameObject[] commonEnemy;
    public GameObject[] rareEnemy;
    public GameObject[] RarestEnemy;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        if(amountEnemies > 0)
            enemySpawner = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<EnemySpawner>();
    }

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
            
            if(amountEnemies > 0)
            {
                for (int i = 0; i < amountEnemies; i++)
                {
                    spawnEnemy();
                }
            }
        }
    }

    private void spawnEnemy()
    {
        int randomChance = Random.Range(0, 100);
        if (randomChance <= 50 && randomChance > 0)
        {
            enemySpawner.spawnSingleEnemy(commonEnemy[Random.Range(0, commonEnemy.Length)]);
            return;
        }
        if (randomChance <= 85 && randomChance > 50)
        {
            enemySpawner.spawnSingleEnemy(rareEnemy[Random.Range(0, rareEnemy.Length)]);
            return;
        }
        if (randomChance <= 100 && randomChance > 85)
        {
            enemySpawner.spawnSingleEnemy(RarestEnemy[Random.Range(0, RarestEnemy.Length)]);
            return;
        }
    }
}
