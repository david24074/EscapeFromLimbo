using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateOnEnter : MonoBehaviour
{
    [Header("Activation requirements")]
    public bool requireNoActiveEnemies;

    [Header("Activate objects")]
    [SerializeField] private bool activateObjects;
    [SerializeField] private GameObject[] objectsToActivate;

    [Header("Deactivate objects")]
    [SerializeField] private GameObject deactivateObjects;
    [SerializeField] private GameObject[] objectsToDeactivate;

    [Header("Load scene options")]
    [SerializeField] private bool loadScene;
    [SerializeField] private int sceneIndex;

    [Header("Camera options")]
    [SerializeField] private Transform newCameraPos;

    [Header("Player Options")]
    [SerializeField] private bool activatePlayerScript;
    [SerializeField] private bool deactivatePlayerScript;

    [Header("Spawn enemy options")]
    [SerializeField] private int amountEnemies;
    [SerializeField] private GameObject[] commonEnemy;
    [SerializeField] private GameObject[] rareEnemy;
    [SerializeField] private GameObject[] RarestEnemy;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        if(amountEnemies > 0)
            enemySpawner = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<EnemySpawner>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(requireNoActiveEnemies)
                if (GameObject.FindGameObjectWithTag("Enemy"))
                     return;

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

            if (deactivatePlayerScript)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
            }

            if (activatePlayerScript)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = true;
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

            Destroy(transform.gameObject);
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
