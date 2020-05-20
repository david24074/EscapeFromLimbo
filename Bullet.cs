using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public bool destroyOnImpact;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyAI>().takeDamage(Damage, transform.gameObject);
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
}
