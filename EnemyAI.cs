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

    [Header("Limb options")]
    public GameObject[] bodyParts;

    [Header("Attack options")]
    public float damage;
    public float resetTimer;
    public GameObject damageInstance;
    public GameObject Barrel;
    private bool mayAttack = true;

    [Header("Statistics")]
    public bool hurt;
    public float health = 100;
    private bool isDead, walking;
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
        if(agent.remainingDistance < agent.stoppingDistance)
        {
            if(player && mayAttack)
            {
                if(Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance)
                {
                    mayAttack = false;
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        GameObject bullet = Instantiate(damageInstance, Barrel.transform.position, Quaternion.identity);
        player.GetComponent<Player>().takeDamage(damage);
        StartCoroutine(resetAttack(resetTimer));
    }

    private IEnumerator resetAttack(float timer)
    {
        yield return new WaitForSeconds(timer);
        mayAttack = true;
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
                if (!obj.GetComponent<Collider>())
                {
                    obj.AddComponent<BoxCollider>();
                }
                float directionX = Random.Range(-8f, 8f);
                float directionY = Random.Range(-4f, 4f);

                rb.AddForce(new Vector3(directionX, directionY).normalized *35);
                Destroy(obj, 3);
            }
            //player.GetComponent<Player>().addToScore(50);
            Destroy(transform.gameObject);
        }
    }

    public IEnumerator resetMonitor()
    {
        yield return new WaitForSeconds(0.35f);
        hurt = false;
        monitorRenderer.material = whiteMat;
        //player.GetComponent<Player>().addToScore(10);
    }
}
