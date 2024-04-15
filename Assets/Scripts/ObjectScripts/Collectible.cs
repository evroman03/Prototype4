using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private ScoreManager scoreManager;
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager component not found in the scene.");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the tag "Good"
        if (other.CompareTag("Player"))
        {
            // Output a debug message
            scoreManager.ChangeScore(50);
        }
    }
}