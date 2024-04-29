using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    [SerializeField] float maxTime = 180;
    [SerializeField] float timeRemaining;
    [SerializeField] bool timerIsRunning = false;
    [SerializeField] TMP_Text timeText;

    private void Start()
    {
        TimerStart();
    }

    public void TimerStart()
    {
        timerIsRunning = true;
        timeRemaining = maxTime;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                SceneManager.LoadScene(2);
                SoundManager.Instance.ChaseCompleted();
            }
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}
