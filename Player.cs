﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player statistics")]
    [SerializeField] private float health;
    [SerializeField] private float rotateSpeed = 10;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private Material hurtMaterial;
    private Material idleMaterial;
    private MeshRenderer renderer;
    private int layer_mask;
    private Vector3 inputMovement;
    private bool isDead, inSafeArea;
    private Rigidbody rb;

    [Header("Weapons")]
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject sniperRifle;
    [SerializeField] private GameObject assaultRifle;
    [SerializeField] private GameObject rocketLauncher;
    [SerializeField] private GameObject miniGun;
    private GameObject activeWeapon;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        idleMaterial = renderer.material;
        MusicMGR.FindFilter();
        rb = GetComponent<Rigidbody>();
        layer_mask = LayerMask.GetMask("Ground", "Enemy");
        if (ES3.KeyExists("weaponIndex"))
        {
            int index = ES3.Load<int>("weaponIndex");
            setWeapon(index, null);
        }
        else
        {
            activeWeapon = pistol;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
        {
            var lookPos = hit.point - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        }

        inputMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(inputMovement * Time.deltaTime * speed, Space.World);

        if (!inSafeArea)
            TakeDamage(0.1f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
                
    }

    public void Jump()
    {
        if (CheckIfGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public bool CheckIfReloadingGun()
    {
        return activeWeapon.GetComponent<Pistol>().CheckIfReloading();
    }

    private bool CheckIfGrounded()
    {
        float DisstanceToTheGround = GetComponent<Collider>().bounds.extents.y;
        return (Physics.Raycast(transform.position, -transform.up, DisstanceToTheGround + 0.1f));
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            renderer.material = hurtMaterial;
            StartCoroutine(BackToIdleMaterial());
        }
    }

    private IEnumerator BackToIdleMaterial()
    {
        yield return new WaitForSeconds(0.25f);
        renderer.material = idleMaterial;
    }

    private void Die()
    {
        isDead = true;
        ES3.Save<int>("weaponIndex", 1);
        SceneMGR.ResetScene();
    }

    private void OnTriggerEnter(Collider other)
    {
        inSafeArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inSafeArea = false;
    }

    private void OnTriggerStay(Collider other)
    {
        inSafeArea = false;

        if (other.tag == "Death" && !isDead)
            Die();

        if (other.tag == "Lever")
        {
            if (Input.GetKey(KeyCode.E))
            {
                other.GetComponent<Lever>().activateLever();
            }
            inSafeArea = true;
            return;
        }

        if (other.tag == "Trap")
        {
            if (!isDead)
                transform.position = other.transform.GetComponent<ResetToWaypoint>().GetWaypoint().position;
            inSafeArea = true;
            return;
        }

        if (other.tag == "Pickup")
            inSafeArea = true;

        if (other.tag == "Enemy")
            inSafeArea = true;

        if (other.tag == "LightArea")
            inSafeArea = true;
        
    }

    public void setWeapon(int weaponIndex, GameObject pickup)
    {
        if(activeWeapon)
            Instantiate(activeWeapon.GetComponent<Pistol>().GetGunDrop(), transform.position, Quaternion.identity);
        pistol.SetActive(false);
        assaultRifle.SetActive(false);
        sniperRifle.SetActive(false);
        rocketLauncher.SetActive(false);
        miniGun.SetActive(false);
        switch (weaponIndex)
        {
            case 1:
                pistol.SetActive(true);
                activeWeapon = pistol;
                ES3.Save<int>("weaponIndex", 1);
                if (pickup)
                    Destroy(pickup);
                break;
            case 2:
                assaultRifle.SetActive(true);
                activeWeapon = assaultRifle;
                ES3.Save<int>("weaponIndex", 2);
                if (pickup)
                    Destroy(pickup);
                break;
            case 3:
                sniperRifle.SetActive(true);
                activeWeapon = sniperRifle;
                ES3.Save<int>("weaponIndex", 3);
                if (pickup)
                    Destroy(pickup);
                break;
            case 4:
                rocketLauncher.SetActive(true);
                activeWeapon = rocketLauncher;
                ES3.Save<int>("weaponIndex", 4);
                if (pickup)
                    Destroy(pickup);
                break;
            case 5:
                miniGun.SetActive(true);
                activeWeapon = miniGun;
                ES3.Save<int>("weaponIndex", 5);
                if (pickup)
                    Destroy(pickup);
                break;
        }
    }

}
