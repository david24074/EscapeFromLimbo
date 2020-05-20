using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public int maxSize = 300;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void spawnEnemies(int amount, GameObject newEnemy)
    {
        enemy = newEnemy;
        for(int i = 0; i < amount; i++)
        {
            Instantiate(enemy, new Vector3(Random.Range(transform.position.x, transform.position.x + maxSize), 0, Random.Range(transform.position.z, transform.position.z - 60)), Quaternion.identity);
        }
    }

    public void spawnSingleEnemy(GameObject newEnemy)
    {
        enemy = newEnemy;
        Instantiate(enemy, new Vector3(Random.Range(transform.position.x, transform.position.x + maxSize), 0, Random.Range(transform.position.z, transform.position.z - 60)), Quaternion.identity);
    }
}
