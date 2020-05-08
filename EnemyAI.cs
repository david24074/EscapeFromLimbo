using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Material")]
    public MeshRenderer monitorRenderer;
    public Material whiteMat;
    public Material redmat;

    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 playerPos;

    public GameObject[] bodyParts;

    public bool hurt;
    public float health = 100;
    private bool isDead;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        playerPos = player.transform.position;
        agent.SetDestination(playerPos);
    }

    private void Update()
    {
        if(playerPos != player.transform.position)
        {
            playerPos = player.transform.position;
            agent.SetDestination(playerPos);
        }
    }

    public void takeDamage(float damage, GameObject bullet)
    {
        hurt = true;
        monitorRenderer.material = redmat;
        StartCoroutine(resetMonitor());
        health -= damage;
        if (health <= 0 && !isDead)
        {
            isDead = true;
            foreach (GameObject obj in bodyParts)
            {
                obj.transform.SetParent(null);
                Rigidbody rb = obj.AddComponent<Rigidbody>();
                obj.AddComponent<BoxCollider>();
                rb.AddForce(transform.position - bullet.transform.position * 35);
                Destroy(obj, 3);
            }
            player.GetComponent<Player>().addToScore(50);
            Destroy(transform.gameObject);
        }
    }

    public IEnumerator resetMonitor()
    {
        yield return new WaitForSeconds(0.35f);
        hurt = false;
        monitorRenderer.material = whiteMat;
        player.GetComponent<Player>().addToScore(10);
    }
}
