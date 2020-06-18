using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Pistol objects")]
    [SerializeField] private GameObject barrel;
    [SerializeField] private GameObject bullet;
    public GameObject gunDrop;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip noAmmoSound;

    [Header("Gun statistics")]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float ammoPerClip = 12;
    [SerializeField] private float reloadTime = 1;
    [SerializeField] private float damage = 12;
    [SerializeField] private float bulletFireSpeed = 10;
    [SerializeField] private bool isAutomatic;
    [SerializeField] private bool destroyBulletOnHit;

    private float fireRateSave, currentAmmo;
    private bool reloading;

    private void Start()
    {
        currentAmmo = ammoPerClip;
        fireRateSave = fireRate;
    }

    private void Update()
    {
        fireRate -= 1 * Time.deltaTime;
        if(fireRate <= 0 && currentAmmo > 0 && !reloading)
        {
            if (isAutomatic)
            {
                if (Input.GetMouseButton(0))
                {
                    FireBullet();
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    FireBullet();
                }
            }
         
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!reloading)
            {
                reloading = true;
                StartCoroutine(reload());
            }
        }
    }

    public bool checkIfReloading()
    {
        return reloading;
    }

    private void FireBullet()
    {
        if (fireSound)
            MusicMGR.playAudioClip(fireSound, 0, 0, 0, false);
        fireRate = fireRateSave;
        currentAmmo -= 1;
        GameObject newBullet = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletFireSpeed);
        newBullet.GetComponent<Bullet>().setStats(damage, destroyBulletOnHit);
        Destroy(newBullet, 3);
    }

    private IEnumerator reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoPerClip;
        reloading = false;
    }
}
