using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float rotateSpeed = 10, speed = 10;
    private int layer_mask;
    private Vector3 inputMovement;
    public float score;

    public TextMeshProUGUI scoreText;

    private void Start()
    {
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
