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
    private enum EnemyState
    {
        None, MovingLeft, MovingRight, MovingForward,
    }
    public void GameReady()
    {
        gC = GameController.Instance;
        currentSnap = gC.EnemyCenterSnap;
        enemy = gC.Enemy;
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
