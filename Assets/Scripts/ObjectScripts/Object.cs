using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int ScoreChangeFactor;
    public float EnemyCarDistanceChange;
    public bool isBarrel;
    private Rigidbody rb;
    private GameObject moveTowards;
    private EnemyCarController enemyCar;


    private void Start()
    {
        if (isBarrel)
        {
            enemyCar = GameController.Instance.Enemy.GetComponentInChildren<EnemyCarController>();
            rb = GetComponent<Rigidbody>();
            for(int i =0; i < LaneManager.Instance.LaneEnds.Length; i++)
            {
                if(enemyCar.currentSnap == i)
                {
                    moveTowards = LaneManager.Instance.LaneEnds[i];
                }
            }
        }

    }
    private void Update()
    {
        if (isBarrel)
        {
            var step = (GameController.Instance.BackgroundSpeed/2) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(rb.position, moveTowards.transform.position, step);
        } 
    }

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
        if(other.CompareTag("LaneEnd"))
        {
            DestroyThis();
        }
    }
    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
