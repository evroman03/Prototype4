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
    [SerializeField] int targetscore = 0; // Computers score (total score)
    [SerializeField] int currentscore = 0;// The current score (coroutines score)
    [SerializeField] float currentMultiplier = 1; // The current multiplier
    //[SerializeField] float timeSinceHit;
    [SerializeField] int multLevelsToRemoveOnHit = 1;
    public int[] totalTimeToNextMultLevel;
    public float[] multiplierAmounts;
    public float currentScoreTime;
    public int currentMultIndex;

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
            currentScoreTime += Time.deltaTime;
            if(currentMultIndex < totalTimeToNextMultLevel.Length && currentScoreTime >= totalTimeToNextMultLevel[currentMultIndex])
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
        targetscore += (int)(points * currentMultiplier);
        if(targetscore < 0)
        {
            targetscore = 0;
        }
        SaveScore();
    }

    public void DecreaseMultiplier()
    {
        currentScoreTime = 0;

        currentMultiplier += -multLevelsToRemoveOnHit;
        if (currentMultiplier <= 0)
        {
            currentMultiplier = 1;
        }
        UpdateMultiplierText();
    }    

    // Function to update the score text in the UI
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentscore.ToString();
        }
    }

    private void UpdateMultiplierText()
    {
        if(multiplierText != null)
        {
            multiplierText.text = "x" + currentMultiplier.ToString();
        }
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt("Score", currentscore);
        PlayerPrefs.Save();
    }
    IEnumerator StartOdometer()
    {
        while(true)
        {
            if (currentscore < targetscore)
            {
                currentscore += UIScoreStepAmount;
                if (currentscore > targetscore)
                {
                    currentscore = targetscore;
                }
                UpdateScoreText();
            }
            if (currentscore > targetscore)
            {
                currentscore -= UIScoreStepAmount;
                if (currentscore < targetscore)
                {
                    currentscore = targetscore;
                }
                UpdateScoreText();
            }
            yield return new WaitForSeconds(UIScoreStepTime);
        }
    }
}