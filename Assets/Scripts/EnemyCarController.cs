using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    private int currentSnap;
    private GameController gC;
    private GameObject enemy;
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
        gC = GameController.Instance;
        currentSnap = gC.EnemyCenterSnap;
        enemy = gC.Enemy;
        currentState = EnemyState.MovingLeft;
        currentCoroutine = StartCoroutine(MovingLeft());

    }
    public void GSM(EnemyState state)
    {
        switch(state)
        {
            case EnemyState.None:
                StopCoroutine(currentCoroutine);
                currentState = EnemyState.None;
                break;
            case EnemyState.MovingLeft:
                StopCoroutine(currentCoroutine);    
                currentState = EnemyState.MovingLeft;
                currentCoroutine = StartCoroutine(MovingLeft());
                break;
            case EnemyState.MovingRight:
                StopCoroutine(currentCoroutine);
                currentState = EnemyState.MovingRight;
                currentCoroutine = StartCoroutine(MovingRight());
                break;
            case EnemyState.MovingForward:
                StopCoroutine(currentCoroutine);
                currentState = EnemyState.MovingForward;
                currentCoroutine = StartCoroutine(MovingForward());
                break;
            case EnemyState.MovingBackward:
                StopCoroutine(currentCoroutine);
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







    public bool CanMoveLeft()
    {
        return (currentSnap != 0);
    }
    public bool CanMoveRight()
    {
        return (currentSnap != gC.EnemySnaps.Length - 1);
    }
    public void MoveLeft()
    {
        if (CanMoveLeft())
        {
            currentSnap -= 1;
            enemy.transform.position = new Vector3(gC.EnemySnaps[currentSnap].transform.position.x, gC.EnemySnaps[currentSnap].transform.position.y, enemy.transform.position.z);
        }
    }
    public void MoveRight()
    {
        if (CanMoveRight())
        {
            currentSnap += 1;
            enemy.transform.position = new Vector3(gC.EnemySnaps[currentSnap].transform.position.x, gC.EnemySnaps[currentSnap].transform.position.y, enemy.transform.position.z);
        }
    }
    public void ChangeDistance(float distance)
    {
        if(distance != 0)
        {

        }
    }
    public void SpawnBarrel(GameObject barrel)
    {
        if(barrel != null)
        {
            Instantiate(barrel, barrelSpawn);
        }
    }
}
