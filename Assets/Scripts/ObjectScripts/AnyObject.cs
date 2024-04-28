using System;
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
        None, One, Two, Half, All, 
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
        //The enums are purely for Game Dev's ease of access, esp so they dont put in an illegal number
        switch(multLevelsToReduce)
        {
            case MultLevelsToReduce.None:
                MultiplierLevelsToReduce = 0;
                break;
            case MultLevelsToReduce.One:
                MultiplierLevelsToReduce = 1;
                break;
            case MultLevelsToReduce.Two:
                MultiplierLevelsToReduce = 2;
                break;
            case MultLevelsToReduce.Half:
                MultiplierLevelsToReduce = ScoreManager.Instance.multiplierAmounts.Length/2;
                break;
            case MultLevelsToReduce.All:
                MultiplierLevelsToReduce = ScoreManager.Instance.multiplierAmounts.Length-ScoreManager.Instance.currentMultIndex;
                break;
        }
    }
    private void Update()
    {
        if (isBarrel) 
        {
            var step = (GameController.Instance.currentBGSpeed/2) * Time.deltaTime;
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
            if(EnemyCarDistanceChange > 0 )
            {
                var GC = GameController.Instance;
                //Get the enemy and move him away from the player
                var enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponentInChildren<EnemyCarController>();
                enemy.UpdateDistance(EnemyCarDistanceChange);

                //Affect the background speed 
                GC.ChangeBackgroundSpeed(-GC.speedIncreasePerBGUp[GC.currentBackgroundSpeedIndex]); //reduce the speed by the current index's value
                GC.currentBackgroundSpeedIndex = Mathf.Clamp(GC.currentBackgroundSpeedIndex - 1, 0, GC.timesToNextBGSpeedUps.Length - 1); //THEN reduce the index

                //For the obstacle timer
                GC.currentTime = 0;

                //For the multiplier
                ScoreManager.Instance.DecreaseMultiplier(MultiplierLevelsToReduce);
                //ScoreManager.Instance.currentMultIndex -= MultiplierLevelsToReduce;
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
