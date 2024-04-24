using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnyObject : MonoBehaviour
{
    public int ScoreChangeFactor;
    public float EnemyCarDistanceChange;
    public int BackgroundSpeedChange;
    private int MultiplierLevelsToReduce;
    public bool isBarrel;
    private Rigidbody rb;
    private GameObject moveTowards;
    public EnemyCarController enemyCar;
    public enum MultLevelsToReduce
    {
        None, One, Half, All, 
    }
    public MultLevelsToReduce multLevelsToReduce;

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
        switch(multLevelsToReduce)
        {
            case MultLevelsToReduce.None:
                MultiplierLevelsToReduce = 0;
                break;
            case MultLevelsToReduce.One:
                MultiplierLevelsToReduce = 1;
                break;
            case MultLevelsToReduce.Half:
                break;
            case MultLevelsToReduce.All:
                break;
        }
    }
    private void Update()
    {
        if (isBarrel)
        {
            var step = (GameController.Instance.currentBackgroundSpeed/2) * Time.deltaTime;
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
            if(EnemyCarDistanceChange < 0 )
            {
                //Get the enemy and move him away from the player
                var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<EnemyCarController>();
                enemy.StartChangeDistanceCoroutine(EnemyCarDistanceChange);

                //Affect the background speed 
                GameController.Instance.ChangeBackgroundSpeed(BackgroundSpeedChange);

                //For the obstacle timer
                GameController.Instance.currentTime = 0;
                GameController.Instance.currentBackgroundSpeedIndex--;
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
