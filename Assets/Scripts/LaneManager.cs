using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    #region Singleton
    private static LaneManager instance;
    public static LaneManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(LaneManager)) as LaneManager;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    public GameObject[] PlayerSnaps;
    public GameObject[] EnemySnaps;
    private EnemyCarController enemyCar;
    [HideInInspector] public int PlayerCenterSnap, EnemyCenterSnap;

    public void GameReady()
    {
        PlayerCenterSnap = PlayerSnaps.Length / 2;
        EnemyCenterSnap = EnemySnaps.Length / 2;
        enemyCar = GameController.Instance.Enemy.GetComponentInChildren<EnemyCarController>();
        StartCoroutine(TestLanes());
    }
    private IEnumerator TestLanes()
    {
        while (true)
        {
            OpenLanes();
            print(GameController.Instance.MinSpeed / GameController.Instance.BackgroundSpeed);
            yield return new WaitForSeconds(GameController.Instance.MinSpeed / GameController.Instance.BackgroundSpeed);
        }
    }
    public GameObject[] OpenLanes()
    {
        var openLanes = new GameObject[EnemySnaps.Length]; //Making sure the maximum number of open lanes is as big as however many lanes we have
        for(int i =0; i<EnemySnaps.Length; i++)
        {
            if(!ObstacleAhead(EnemySnaps[i]))
            {
                openLanes[i] = EnemySnaps[i];
            }
            else
            {
                openLanes[i] = null;
            }
        }
        return openLanes;
    }
    private bool ObstacleAhead(GameObject castFrom)
    {
        RaycastHit hit;
        if (Physics.Raycast(castFrom.transform.position, transform.forward, out hit, enemyCar.detectionDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                return true;
            }
        }
        return false;
    }
}
