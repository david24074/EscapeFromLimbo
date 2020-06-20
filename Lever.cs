using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool activated;

    [Header("Object options")]
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private GameObject[] objectsToDestroy;
    [SerializeField] private Animator anim;
    [SerializeField] private Light objectLight;

    [Header("Sound options")]
    [SerializeField] private AudioClip soundToPlay;
    [SerializeField] private int Amount = 0;
    [SerializeField] private int Timer = 0;
    [SerializeField] private int timeAdded = 0;
    [SerializeField] private bool useFilter = true;

    [Header("Enemy Options")]
    [SerializeField] private bool spawnEnemies;
    [SerializeField] private int amountEnemies;
    [SerializeField] private GameObject[] commonEnemy;
    [SerializeField] private GameObject[] rareEnemy;
    [SerializeField] private GameObject[] RarestEnemy;
    private EnemySpawner enemySpawner;

    public void activateLever()
    {
        if (!activated)
        {
            activated = true;
            anim.Play("Activate");
            foreach(GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in objectsToDestroy)
            {
                Destroy(obj);
            }

            MusicMGR.PlayAudioClip(soundToPlay, Amount, Timer, timeAdded, useFilter);

            if (spawnEnemies)
            {


                for (int i = 0; i < amountEnemies; i++)
                {
                    SpawnEnemy();
                }
            }
        }
    }

    private void SpawnEnemy()
    {
        if (!enemySpawner)
            enemySpawner = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<EnemySpawner>();

        int randomChance = Random.Range(0, 100);
        if (randomChance <= 50 && randomChance > 0)
        {
            enemySpawner.SpawnSingleEnemy(commonEnemy[Random.Range(0, commonEnemy.Length)]);
            return;
        }
        if (randomChance <= 85 && randomChance > 50)
        {
            enemySpawner.SpawnSingleEnemy(rareEnemy[Random.Range(0, rareEnemy.Length)]);
            return;
        }
        if (randomChance <= 100 && randomChance > 85)
        {
            enemySpawner.SpawnSingleEnemy(RarestEnemy[Random.Range(0, RarestEnemy.Length)]);
            return;
        }
    }
}
