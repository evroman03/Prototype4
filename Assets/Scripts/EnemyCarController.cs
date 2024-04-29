using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCarController : MonoBehaviour
{
    [HideInInspector] public int currentSnap;
    private LaneManager LM;
    private GameObject enemy;
    private float minZ, maxZ, targetZ;
    public float AIDetectionDistance, DistMaxToOrigin, DistMinToOrigin, OriginDistFromPlayer, moveStep, timeStep, catchDistPlayerToEnemy, currentCatchTime, catchTimeToWin;
    [Tooltip("How much time you lose when not catching the enemy. Recommend 0-0.5f. Formula is timeLost = time.dT * catchTimeLoss")] public float catchTimeLoss;
    public Transform barrelSpawn;
    public GameObject[] ThingsToThrow;
    public Animator MonkeAnimator;
    public void GameReady()
    {
        LM = LaneManager.Instance;
        currentSnap = LM.EnemyCenterSnap;
        enemy = GameController.Instance.Enemy;

        targetZ = OriginDistFromPlayer;
        minZ = targetZ - DistMinToOrigin;
        maxZ = targetZ + DistMaxToOrigin;

        currentCatchTime = catchTimeToWin;

        StartCoroutine(AvoidObstacle());
        StartCoroutine(SpawnStuff());
        StartCoroutine(ChangeDistanceOverTime());
        StartCoroutine(CatchMechanic());
    }
    private IEnumerator SpawnStuff()
    {
        while (true)
        {
            //MonkeAnimator.SetTrigger("ThrowBarrel");
            SpawnBarrel();
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.75f, 3f));
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
    }
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
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z + moveStep);
            }
            else if (targetZ < enemy.transform.position.z)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z - moveStep);
            }
            yield return new WaitForSeconds(timeStep);
        }
    }
    /// <summary>
    /// This should probably be in the score manager.
    /// </summary>
    /// <returns></returns>
    public IEnumerator CatchMechanic()
    {
        while(true)
        {
            if(enemy.transform.position.z <= catchDistPlayerToEnemy)
            {
                if (!ScoreManager.Instance.catchingText.gameObject.activeSelf)
                {
                    ScoreManager.Instance.catchingText.gameObject.SetActive(true);
                }         
                currentCatchTime -= Time.deltaTime;
                ScoreManager.Instance.UpdateCatchText(currentCatchTime);
                if (currentCatchTime <= 0)
                {                  
                    SceneManager.LoadScene(2);
                    SoundManager.Instance.ChaseCompleted();
                }
            }
            else
            {
                if(ScoreManager.Instance.catchingText.gameObject.activeSelf)
                {
                    ScoreManager.Instance.catchingText.gameObject.SetActive(false);
                }
                currentCatchTime = Mathf.Clamp(currentCatchTime + Time.deltaTime * catchTimeLoss, 0, int.MaxValue);
            }
            yield return null;
        }
    }
    public void SpawnBarrel()
    {
        var barrel = ThingsToThrow[UnityEngine.Random.Range(0, ThingsToThrow.Length)];
        Instantiate(barrel, barrelSpawn.position, Quaternion.identity);
    }
}
