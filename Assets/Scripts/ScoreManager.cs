using UnityEngine;
using TMPro;
using System.Collections;

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
    public int UIScoreStepAmount = 5; // Amount that the score will increment/decrement
    public float UIScoreStepTime = 0.01f; // How fast the score will increment/decrement
    public int[] totalTimeToNextMultLevel;
    public float[] multiplierAmounts;
    [HideInInspector] public int targetScore, currentScore, currentMultIndex;
    [HideInInspector] public float currentMultTime, currentMultiplier;


    private void Start()
    {
        UpdateScoreText();
        UpdateMultiplierText();
        StartCoroutine(StartOdometer());
        StartCoroutine(RunMultiplier());
    }
    private IEnumerator RunMultiplier()
    {
        while(true)
        {
            currentMultTime += Time.deltaTime;
            if(currentMultIndex < totalTimeToNextMultLevel.Length-1 && currentMultTime >= totalTimeToNextMultLevel[currentMultIndex])
            {
                currentMultiplier = multiplierAmounts[currentMultIndex];
                currentMultIndex++;
                UpdateMultiplierText();

            }
            yield return null;
        }
    }
    //private void Update()
    //{
    //    timeSinceHit += Time.deltaTime;
    //    if(timeSinceHit >= 15 && timeSinceHit < 30)
    //    {
    //        multiplier = 2;
    //        UpdateMultiplierText();
            
    //    }
    //    else if(timeSinceHit >= 30 && timeSinceHit < 45)
    //    {
    //        multiplier = 3;
    //        UpdateMultiplierText();

    //    }
    //    else if(timeSinceHit >= 45 && timeSinceHit < 60)
    //    {
    //        multiplier = 4;
    //        UpdateMultiplierText();

    //    }
    //}
    // Function to add points to the score
    public void ChangeScore(float points)
    {
        targetScore += (int)(points * currentMultiplier);
        if(targetScore < 0)
        {
            targetScore = 0;
        }
        SaveScore();
    }

    public void DecreaseMultiplier(int levelsToRemove)
    {
        currentMultTime = 0;
        currentMultIndex = Mathf.Clamp(currentMultIndex - levelsToRemove, 0, multiplierAmounts.Length);
        UpdateMultiplierText();
    }    

    // Function to update the score text in the UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    private void UpdateMultiplierText()
    {
        if(multiplierText != null)
        {
            multiplierText.text = "x" + multiplierAmounts[currentMultIndex].ToString();
        }
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", currentScore);
        PlayerPrefs.Save();
    }
    IEnumerator StartOdometer()
    {
        while(true)
        {
            if (currentScore < targetScore)
            {
                currentScore += UIScoreStepAmount;
                if (currentScore > targetScore)
                {
                    currentScore = targetScore;
                }
                UpdateScoreText();
            }
            if (currentScore > targetScore)
            {
                currentScore -= UIScoreStepAmount;
                if (currentScore < targetScore)
                {
                    currentScore = targetScore;
                }
                UpdateScoreText();
            }
            yield return new WaitForSeconds(UIScoreStepTime);
        }
    }
}