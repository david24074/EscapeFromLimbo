using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] private enum gunType { pistol, sniperRifle, assaultRifle, rocketLauncher, miniGun }
    [SerializeField] private gunType type;
    [SerializeField] private Light lightNotification;
    private bool interactable;

    private void Awake()
    {
        StartCoroutine(setActive());
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && interactable)
        {
            lightNotification.intensity = 5;
            if (Input.GetKey(KeyCode.E))
            {
                setPlayerWeapon();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            lightNotification.intensity = 1;
        }
    }

    private void setPlayerWeapon()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().checkIfReloadingGun())
        {
            if (type == gunType.pistol)
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setWeapon(1, transform.gameObject);
            if (type == gunType.assaultRifle)
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setWeapon(2, transform.gameObject);
            if (type == gunType.sniperRifle)
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setWeapon(3, transform.gameObject);
            if (type == gunType.rocketLauncher)
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setWeapon(4, transform.gameObject);
            if (type == gunType.miniGun)
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().setWeapon(5, transform.gameObject);
        }
    }

    private IEnumerator setActive()
    {
        yield return new WaitForSeconds(2.5f);
        interactable = true;
    }
}
