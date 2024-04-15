using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(ScoreManager)) as ScoreManager;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    public TMP_Text scoreText; // Reference to the TextMeshPro Text component where the score will be displayed
    public TMP_Text multiplierText; //Reference to the TextMeshPro Text component where the multiplier will be displayed
    private int score = 0; // The current score
    private int multiplier = 1; // The current multiplier

    private void Start()
    {
        UpdateScoreText();
        UpdateMultiplierText();
    }
    // Function to add points to the score
    public void ChangeScore(int points)
    {
        score += points * multiplier;
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

    private void UpdateMultiplierText()
    {
        if(multiplierText != null)
        {
            multiplierText.text = "x" + multiplier.ToString();
        }
    }
}