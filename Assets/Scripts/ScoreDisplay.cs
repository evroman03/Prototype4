using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;
    public int currentScore;
    public int targetScore;
    public int UIScoreStepAmount = 5;
    public float UIScoreStepTime = 0.01f;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(StartOdometer());
    }
    private void LoadScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }
    IEnumerator StartOdometer()
    {
        targetScore = PlayerPrefs.GetInt("Score", 0);
        while (true)
        {
            if (currentScore < targetScore)
            {
                currentScore += UIScoreStepAmount;
                if (currentScore > targetScore)
                {
                    currentScore = targetScore;
                }
                LoadScore();
            }
            yield return new WaitForSeconds(UIScoreStepTime);
        }
    }
}
