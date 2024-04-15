using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; // Reference to the TextMeshPro Text component where the score will be displayed
    public TMP_Text multiplierText; //Reference to the TextMeshPro Text component where the multiplier will be displayed
    private int score = 0; // The current score
    private int multiplier = 1; // The current multiplier
    private float timeSinceHit;
    [SerializeField] int levelsToRemove = 1;

    private void Start()
    {
        UpdateScoreText();
        UpdateMultiplierText();
    }
    private void Update()
    {
        if(timeSinceHit >= 15 || timeSinceHit < 30)
        {
            multiplier = 2;
        }
        else if(timeSinceHit >= 30 || timeSinceHit < 45)
        {
            multiplier = 3;
        }
        else if(timeSinceHit >= 45 || timeSinceHit < 60)
        {
            multiplier = 4;
        }
    }
    // Function to add points to the score
    public void ChangeScore(int points)
    {
        score += points * multiplier;
        UpdateScoreText();
    }

    private void OnTriggerEnter(Collider other)
    {
        multiplier += -levelsToRemove;
        if (multiplier <= 0)
        {
            multiplier = 1;
        }
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