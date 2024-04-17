using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    [HideInInspector] public int currentSnap;
    private LaneManager LM;
    private GameObject enemy;
    public float detectionDistance;
    public Transform barrelSpawn;
    public GameObject[] ThingsToThrow;
    public enum EnemyState
    {
        None, MovingLeft, MovingRight, MovingForward, MovingBackward, InCatchZone, HitObstacle
    }
    public EnemyState currentState;
    public Coroutine currentCoroutine;

    public void GameReady()
    {
        LM = LaneManager.Instance;
        currentSnap = LM.EnemyCenterSnap;
        enemy = GameController.Instance.Enemy;
        StartCoroutine(AvoidObstacle());
        StartCoroutine(SpawnStuff());
        //currentState = EnemyState.MovingLeft;
        //currentCoroutine = StartCoroutine(MovingLeft());
    }
    public void GSM(EnemyState state)
    {
        if(currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        switch(state)
        {
            case EnemyState.None:
                currentState = EnemyState.None;
                break;
            case EnemyState.MovingLeft:  
                currentState = EnemyState.MovingLeft;
                currentCoroutine = StartCoroutine(MovingLeft());
                break;
            case EnemyState.MovingRight:
                currentState = EnemyState.MovingRight;
                currentCoroutine = StartCoroutine(MovingRight());
                break;
            case EnemyState.MovingForward:
                currentState = EnemyState.MovingForward;
                currentCoroutine = StartCoroutine(MovingForward());
                break;
            case EnemyState.MovingBackward:
                currentState = EnemyState.MovingBackward;
                currentCoroutine = StartCoroutine(MovingBackward());
                break;
            case EnemyState.InCatchZone:
                StopCoroutine(currentCoroutine);
                currentState = EnemyState.InCatchZone;
                break;
        }
    }
    private IEnumerator MovingLeft()
    {
        print(currentState.ToString());
        while (currentState == EnemyState.MovingLeft)
        {
            GSM(EnemyState.MovingRight);
            yield return null;
        }
    }
    private IEnumerator MovingRight()
    {
       
        while (currentState == EnemyState.MovingRight)
        {

            GSM(EnemyState.MovingForward);

            yield return null;
        }

    }
    private IEnumerator MovingForward()
    {
        while (currentState == EnemyState.MovingForward)
        {
            GSM(EnemyState.MovingBackward);
            yield return null;
        }
    }
    private IEnumerator MovingBackward()
    {
        while (currentState == EnemyState.MovingBackward)
        {
            GSM(EnemyState.MovingLeft);
            yield return null;
        }
    }
    private IEnumerator InCatchZone()
    {
        while (currentState == EnemyState.InCatchZone)
        {
            yield return null;
        }
    }
    private IEnumerator HitObstacle()
    {
        while (currentState == EnemyState.HitObstacle)
        {
            yield return null;
        }
    }

    private IEnumerator SpawnStuff()
    {
        while (true)
        {
            SpawnBarrel(ThingsToThrow[UnityEngine.Random.Range(0, ThingsToThrow.Length)]);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.25f, 3f));
        }
    }
    private IEnumerator AvoidObstacle()
    {
        while(true)
        {
            if(LM.ObstacleAheadOfEnemy())
            {
                int laneToGo =  UnityEngine.Random.Range(0, LM.OpenLanes.Length);
                if (LM.OpenLanes[laneToGo] != null)
                {
                    int moveCount=0;
                    if (laneToGo < currentSnap)
                    {
                        moveCount = currentSnap - laneToGo;
                        for (int i = 0; i < moveCount; i++)
                        {
                            MoveLeft();
                        }
                    }
                    else if (laneToGo > currentSnap)
                    {
                        moveCount = laneToGo-currentSnap;
                        for (int i = 0; i < moveCount; i++)
                        {
                            MoveRight();
                        }
                    }    
                }
            }
            yield return new WaitForFixedUpdate();
        }    
    }

    public void MoveLeft()
    {
        if (LM.CanMoveLeft())
        {
            currentSnap -= 1;
            enemy.transform.position = new Vector3(LM.EnemySnaps[currentSnap].transform.position.x, LM.EnemySnaps[currentSnap].transform.position.y, enemy.transform.position.z);
        }
    }
    public void MoveRight()
    {
        if (LM.CanMoveRight())
        {
            currentSnap += 1;
            enemy.transform.position = new Vector3(LM.EnemySnaps[currentSnap].transform.position.x, LM.EnemySnaps[currentSnap].transform.position.y, enemy.transform.position.z);
        }
    }
    public IEnumerator ChangeDistanceOverTime(float distance)
    {
        print("HERE2");
        float duration = 1f;
        float elapsedTime = 0.0f;
        Vector3 initialPosition = enemy.transform.position;
        Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - distance);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            enemy.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            print(t);
            yield return null; // Wait for the next frame
        }

        // Ensure we reach the exact target position
        enemy.transform.position = targetPosition;
        print("HERE3");
    }
    public void SpawnBarrel(GameObject barrel)
    {
        if(barrel != null)
        {
            Instantiate(barrel, barrelSpawn);
        }
    }
}
