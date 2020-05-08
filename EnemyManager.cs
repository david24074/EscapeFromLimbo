using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    public static int totalMaxEnemies = 300;
    public static int lastRoundEnemies;
    public static int currentEnemies = 0;

    public TextMeshProUGUI roundText;
    private int roundNumber;
    private bool roundActive, startingRound;

    private void Start()
    {
        currentEnemies = 0;
        totalMaxEnemies = 0;
        lastRoundEnemies = totalMaxEnemies;
        roundNumber = 0;
        roundText.text = "Start new round by pressing spacebar";
    }

    private void Update()
    {
        if (roundActive)
        {
            if(totalMaxEnemies <= 0)
            {
                roundActive = false;
                GameObject[] remaningEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(GameObject enemy in remaningEnemies)
                {
                    Destroy(enemy);
                }
            }
        }
        else
        {
            if (!roundActive && !startingRound)
            {
                roundText.text = "Start new round by pressing spacebar";
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                roundActive = true;
                StartCoroutine(newRound(3));
            }
        }
    }

    private IEnumerator newRound(int countDown)
    {
        startingRound = true;
        roundText.text = "Next round starting..";
        yield return new WaitForSeconds(countDown);
        startingRound = false;
        roundActive = true;
        roundNumber += 1;
        totalMaxEnemies = 15 * roundNumber;
        Debug.Log(totalMaxEnemies);
        roundText.text = "Round: " + roundNumber.ToString();
    }
}
