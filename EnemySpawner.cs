using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int maxSize = 300;
    private Transform player;
    private Vector3 distToPlayer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        distToPlayer = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + distToPlayer;
    }

    public void SpawnEnemies(int amount, GameObject newEnemy)
    {
        enemy = newEnemy;
        for(int i = 0; i < amount; i++)
        {
            Instantiate(enemy, new Vector3(Random.Range(transform.position.x, transform.position.x + maxSize), transform.position.y, Random.Range(transform.position.z, transform.position.z - 60)), Quaternion.identity);
        }
    }

    public void SpawnSingleEnemy(GameObject newEnemy)
    {
        enemy = newEnemy;
        Instantiate(enemy, new Vector3(Random.Range(transform.position.x, transform.position.x + maxSize), transform.position.y, Random.Range(transform.position.z, transform.position.z - 60)), Quaternion.identity);
    }
}
