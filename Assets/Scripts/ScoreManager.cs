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
    [SerializeField] int score = 0; // The current score
    [SerializeField] int multiplier = 1; // The current multiplier
    [SerializeField] float timeSinceHit;
    [SerializeField] int levelsToRemove = 1;

    private void Start()
    {
        UpdateScoreText();
        UpdateMultiplierText();
    }
    private void Update()
    {
        timeSinceHit += Time.deltaTime;
        if(timeSinceHit >= 15 && timeSinceHit < 30)
        {
            multiplier = 2;
            UpdateMultiplierText();
        }
        else if(timeSinceHit >= 30 && timeSinceHit < 45)
        {
            multiplier = 3;
            UpdateMultiplierText();
        }
        else if(timeSinceHit >= 45 && timeSinceHit < 60)
        {
            multiplier = 4;
            UpdateMultiplierText();
        }
    }
    // Function to add points to the score
    public void ChangeScore(int points)
    {
        score += points * multiplier;
        if(score < 0)
        {
            score = 0;
        }
        UpdateScoreText();
        SaveScore();
    }

    public void DecreaseMultiplier()
    {
        timeSinceHit = 0;
        multiplier += -levelsToRemove;
        if (multiplier <= 0)
        {
            multiplier = 1;
        }
        UpdateMultiplierText();
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

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

}