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
                var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCarController>();
                enemy.ChangeDistanceOverTime(EnemyCarDistanceChange);
            }
            DestroyThis();
        }
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
