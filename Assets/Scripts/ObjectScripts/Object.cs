using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int ScoreChangeFactor;
    public float EnemyCarDistanceChange;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(ScoreChangeFactor != 0)
            {
                ScoreManager.Instance.ChangeScore(ScoreChangeFactor);
            } 
            if(EnemyCarDistanceChange != 0 )
            {
                GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCarController>().ChangeDistance(EnemyCarDistanceChange);
            }
        }
    }
}
