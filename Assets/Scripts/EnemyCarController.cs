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
    public IEnumerator ChangeDistanceOverTime(float distance)
    {
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
    }
    public void SpawnBarrel(GameObject barrel)
    {
        if(barrel != null)
        {
            Instantiate(barrel, barrelSpawn);
        }
    }
}
