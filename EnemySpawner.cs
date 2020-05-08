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

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x - (maxSize / 2), transform.position.y, transform.position.z);
        if(EnemyManager.currentEnemies < EnemyManager.totalMaxEnemies)
        {
            Instantiate(enemy, new Vector3(Random.Range(transform.position.x, transform.position.x + maxSize),0, Random.Range(transform.position.z, transform.position.z - 60)), Quaternion.identity);
            EnemyManager.currentEnemies++;
            EnemyManager.totalMaxEnemies--;
            Debug.Log(EnemyManager.totalMaxEnemies);
        }
    }
}
