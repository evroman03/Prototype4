using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score;
    // Start is called before the first frame update
    private void Start()
    {
        LoadScore();
    }
    private void LoadScore()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
