using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool activated;

    [Header("Object options")]
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDestroy;
    public Animator anim;
    public Light objectLight;

    [Header("Sound options")]
    public AudioClip soundToPlay;
    public int Amount = 0;
    public int Timer = 0;
    public int timeAdded = 0;
    public bool useFilter = true;

    [Header("Enemy Options")]
    public bool spawnEnemies;
    public int amountEnemies;
    public GameObject[] commonEnemy;
    public GameObject[] rareEnemy;
    public GameObject[] RarestEnemy;
    private EnemySpawner enemySpawner;

    private void Start()
    {
        if (spawnEnemies)
        {
            enemySpawner = GameObject.FindGameObjectWithTag("enemySpawner").GetComponent<EnemySpawner>();
        }
    }

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

            MusicMGR.playAudioClip(soundToPlay, Amount, Timer, timeAdded, useFilter);

            if (spawnEnemies)
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
