using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool destroyOnImpact;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().TakeDamage(damage, transform.gameObject);
            if (!destroyOnImpact)
            {
                Destroy(transform.gameObject);
            }
        }
        else
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(transform.gameObject);
        }
        
    }

    public void SetStats(float dam, bool destroyOnHit)
    {
        damage = dam;
        destroyOnImpact = destroyOnHit;
    }
}
