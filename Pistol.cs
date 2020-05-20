using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Pistol objects")]
    public GameObject barrel;
    public GameObject bullet;
    public AudioClip fireSound;

    [Header("Gun statistics")]
    public float fireRate = 0.5f;
    public float ammoPerClip = 12;
    public float reloadTime = 1;
    public float damage = 12;
    public float bulletFireSpeed = 10;

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
            if (Input.GetMouseButton(0))
            {
                if (fireSound)
                    MusicMGR.playAudioClip(fireSound, 0, 0,0, false);
                fireRate = fireRateSave;
                currentAmmo -= 1;
                GameObject newBullet = Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletFireSpeed);
                newBullet.GetComponent<Bullet>().Damage = damage;
                Destroy(newBullet, 3);
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

    private IEnumerator reload()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammoPerClip;
        reloading = false;
    }
}
