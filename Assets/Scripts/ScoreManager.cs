using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the TextMeshPro Text component where the score will be displayed
    private int score = 0; // The current score

    private void Start()
    {
        UpdateScoreText();       
    }
    // Function to add points to the score
    public void ChangeScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Function to update the score text in the UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }
}