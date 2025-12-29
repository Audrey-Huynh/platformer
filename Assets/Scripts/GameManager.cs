using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int scoreToAdd, bool won)
    {
        score += scoreToAdd;
        if (won)
        {
            scoreText.text = "Score: " + score + "! Congrats, you reclaimed your sandcastle!";
        }
        else
        {
            scoreText.text = "Score: " + score;
        }
    }
}
