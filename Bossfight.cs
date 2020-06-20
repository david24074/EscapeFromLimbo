using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bossfight : MonoBehaviour
{
    [SerializeField] private BossAbility[] abilities;
    [Header("Special ability options")]
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private GameObject checkIfAlive, ActivateOnDeath;
    [SerializeField] private EnemyAI healthCheck;
    private float maxHealth;
    private Animator anim;
    private int abilityIndex;
    private Player player;
    private Transform playerTransform;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerTransform = player.transform;
        maxHealth = healthCheck.GetHealth();
        StartCoroutine(startFight());
    }

    private IEnumerator startFight()
    {
        yield return new WaitForSeconds(3.5f);
        NextAbility();
    }

    private void NextAbility()
    {
        if (checkIfAlive)
        {
            if (abilityIndex > abilities.Length - 1)
                abilityIndex = 0;
            BossAbility ability = abilities[abilityIndex];
            abilityIndex++;

            if (ability.objectsToActivate.Length > 0)
            {
                for (int i = 0; i < ability.objectsToActivate.Length; i++)
                {
                    ability.objectsToActivate[i].SetActive(true);
                }
            }

            if (ability.animationNameToPlay != "")
            {
                anim.Play(ability.animationNameToPlay);
            }

            StartCoroutine(EndAbility(ability));
        }
    }

    private void FixedUpdate()
    {
        Vector3 D = playerTransform.position - transform.position;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), 0.5f * Time.deltaTime);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    private void Update()
    {
        if (!checkIfAlive)
        {
            ActivateOnDeath.SetActive(true);
            Destroy(healthSlider);
            Destroy(transform.gameObject);
        }
        else
        {
            healthSlider.value = healthCheck.GetHealth() / maxHealth * 100;
        }
    }

    public void ExplodeGround()
    {
        explosion.gameObject.SetActive(true);
        player.Jump();
    }

    private IEnumerator EndAbility(BossAbility ability)
    {
        yield return new WaitForSeconds(ability.abilityDuration);
        if (ability.objectsToActivate.Length > 0)
        {
            for (int i = 0; i < ability.objectsToActivate.Length; i++)
            {
                ability.objectsToActivate[i].SetActive(false);
            }
        }
        yield return new WaitForSeconds(ability.DurationUntilEnd);
        NextAbility();
    }
}
