using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player statistics")]
    public float health;
    public float rotateSpeed = 10;
    public float speed = 10;
    private int layer_mask;
    private Vector3 inputMovement;

    [Header("Round management")]
    public float score;
    public TextMeshProUGUI scoreText;
    private int currentRound = 0;

    private void Start()
    {
        MusicMGR.findFilter();
        layer_mask = LayerMask.GetMask("Ground", "Enemy");
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
    }

    public void addToScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    public void takeDamage(float amount)
    {
        health -= amount;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (other.tag == "Lever")
            {
                other.GetComponent<Lever>().activateLever();
            }
        }
    }

}
