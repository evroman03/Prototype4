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
    public int scoreStep = 5; // Amount that the score will increment/decrement
    public float step = 0.01f; // How fast the score will increment/decrement
    [SerializeField] int targetscore = 0; // Computers score (total score)
    [SerializeField] int currentscore = 0;// The current score (coroutines score)
    [SerializeField] int multiplier = 1; // The current multiplier
    [SerializeField] float timeSinceHit;
    [SerializeField] int levelsToRemove = 1;

    private void Start()
    {
        UpdateScoreText();
        UpdateMultiplierText();
        StartCoroutine(StartOdometer());
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
        targetscore += points * multiplier;
        if(targetscore < 0)
        {
            targetscore = 0;
        }
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
            scoreText.text = "Score: " + currentscore.ToString();
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
        PlayerPrefs.SetInt("Score", currentscore);
        PlayerPrefs.Save();
    }
    IEnumerator StartOdometer()
    {
        print("HEREFIRST");
        while(true)
        {
            if (currentscore < targetscore)
            {
                print("HERE");
                currentscore += scoreStep;
                if (currentscore > targetscore)
                {
                    currentscore = targetscore;
                }
                UpdateScoreText();
            }
            if (currentscore > targetscore)
            {
                print("HERE2");
                currentscore -= scoreStep;
                if (currentscore < targetscore)
                {
                    currentscore = targetscore;
                }
                UpdateScoreText();
            }
            yield return new WaitForSeconds(step);
        }
    }
}