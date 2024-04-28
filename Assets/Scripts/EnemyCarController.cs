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
    private float minZ, maxZ, targetZ;
    public float detectionDistance, DistMaxToOrigin, DistMinToOrigin, OriginDistFromPlayer, moveStep, timeStep;
    public Transform barrelSpawn;
    public GameObject[] ThingsToThrow;

    public void GameReady()
    {
        LM = LaneManager.Instance;
        currentSnap = LM.EnemyCenterSnap;
        enemy = GameController.Instance.Enemy;
        StartCoroutine(AvoidObstacle());
        StartCoroutine(SpawnStuff());
        StartCoroutine(ChangeDistanceOverTime());
        targetZ = OriginDistFromPlayer;
        minZ = targetZ - DistMinToOrigin;
        maxZ = targetZ + DistMaxToOrigin;
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
    public void UpdateDistance(float distance)
    {
        // a check to make sure the lead car doesnt get too close/far
        targetZ += distance;
        targetZ = Mathf.Clamp(targetZ, minZ, maxZ);
        print("target: " + targetZ + " min: " + minZ + " max: " + maxZ);
        //minZ = minChaseDist - OriginalDistFromPlayer;
        //maxZ = maxChaseDist + OriginalDistFromPlayer;    
        //if (placeToGo > min && placeToGo < max)
        //{
        //    StartCoroutine(ChangeDistanceOverTime(distance));
        //}
    }
    //public IEnumerator ChangeDistanceOverTime(float distance)
    //{
    //    float duration = 1f;
    //    float elapsedTime = 0.0f;
    //    Vector3 initialPosition = enemy.transform.position;
    //    Vector3 targetPosition = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z - distance);
    //    float t = 0;
    //    while (t < 1)
    //    {
    //        t = elapsedTime / duration;
    //        enemy.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
    //        elapsedTime += Time.deltaTime;
    //        yield return null; // Wait for the next frame
    //    }
    //    enemy.transform.position = targetPosition;
    //}
    public IEnumerator ChangeDistanceOverTime()
    {
        if (moveStep == 0)
        {
            moveStep = 0.05f;
        }
        while(true)
        {
            if (targetZ > enemy.transform.position.z)
            {
                //print("HERE1");
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z + moveStep);
            }
            else if (targetZ < enemy.transform.position.z)
            {
                //print("HERE2");
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z - moveStep);
            }
            yield return new WaitForSeconds(timeStep);
        }
    }

    public void SpawnBarrel(GameObject barrel)
    {
        if(barrel != null)
        {
            Instantiate(barrel, barrelSpawn.position, Quaternion.identity);
        }
    }
}
