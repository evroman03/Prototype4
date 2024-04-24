using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(GameController)) as GameController;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    public float MaxSpeed = 50, MinSpeed = 30f, BackgroundSpeed, currentTime;
    public float[] timesToNextSpeedUps;
    public int[] speedIncreasePerUp;
    private int currentBackgroundSpeedIndex = 0;
    public GameObject Player, Enemy;
    public RepeatingBackground[] Backgrounds;


    public GameObject MoveTowards, BackgroundSpawn;



    private RepeatingBackground temp;



    public void Start()
    {
        Application.targetFrameRate = 60;
        SpawnBackground(true);

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Player = Instantiate(Player);
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Enemy = Instantiate(Enemy);  
        }
        LaneManager.Instance.GameReady();
        PlayerController.Instance.GameReady();
        var temp = Enemy.GetComponent<EnemyCarController>();
        temp.GameReady();
        Player.transform.position = LaneManager.Instance.PlayerSnaps[LaneManager.Instance.PlayerCenterSnap].transform.position;
        Enemy.transform.position = LaneManager.Instance.EnemySnaps[LaneManager.Instance.EnemyCenterSnap].transform.position;
        temp.OriginalDist = Enemy.transform.position.z;
        StartCoroutine(ObstacleTimer());
    }
  
    public IEnumerator ObstacleTimer()
    {
        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentBackgroundSpeedIndex < timesToNextSpeedUps.Length && currentTime >= timesToNextSpeedUps[currentBackgroundSpeedIndex])
            {
                ChangeBackgroundSpeed(speedIncreasePerUp[currentBackgroundSpeedIndex]);
                currentBackgroundSpeedIndex++;
            }
            yield return null;
        }
    }
    public void ChangeBackgroundSpeed(int speed)
    {
        BackgroundSpeed = Mathf.Clamp(BackgroundSpeed += speed, MinSpeed, MaxSpeed);
    }
    public void SpawnBackground(bool start)
    {
        if (start)
        {
            var obj2 = Instantiate(Backgrounds[0].gameObject, BackgroundSpawn.transform.position, Quaternion.identity);
            temp = obj2.GetComponent<RepeatingBackground>() ;
        }
        else if (!start)
        {

            var obj = Instantiate(Backgrounds[UnityEngine.Random.Range(0, Backgrounds.Length)], temp.spawnTo.position, Quaternion.identity);
            temp = obj;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeBackgroundSpeed(5);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ChangeBackgroundSpeed(-5);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Enemy.GetComponentInChildren<EnemyCarController>().MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Enemy.GetComponentInChildren<EnemyCarController>().MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //Enemy.GetComponentInChildren<EnemyCarController>().ChangeDistance(-10f);
            Enemy.GetComponentInChildren<EnemyCarController>().StartCoroutine(Enemy.GetComponentInChildren<EnemyCarController>().ChangeDistanceOverTime(-10f));
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Enemy.GetComponentInChildren<EnemyCarController>().ChangeDistance(10f);
            Enemy.GetComponentInChildren<EnemyCarController>().StartCoroutine(Enemy.GetComponentInChildren<EnemyCarController>().ChangeDistanceOverTime(10f));
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
