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

    public float MaxSpeed = 50, MinSpeed = 30f, targetBackgroundSpeed, currentBackgroundSpeed, currentTime, backgroundSpeedPerStep;
    public float[] timesToNextBGSpeedUps;
    public int[] speedIncreasePerBGUp;
    [HideInInspector] public int currentBackgroundSpeedIndex = 0;
    public GameObject Player, Enemy;
    private EnemyCarController eC;
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
        eC = Enemy.GetComponent<EnemyCarController>();
        eC.GameReady();
        Player.transform.position = LaneManager.Instance.PlayerSnaps[LaneManager.Instance.PlayerCenterSnap].transform.position;
        Enemy.transform.position = LaneManager.Instance.EnemySnaps[LaneManager.Instance.EnemyCenterSnap].transform.position;
        eC.OriginalDist = Enemy.transform.position.z;
        StartCoroutine(ObstacleTimer());
        StartCoroutine(SpeedChanger());
        currentBackgroundSpeed = MinSpeed;
        targetBackgroundSpeed = MinSpeed;
    }
  
    public IEnumerator ObstacleTimer()
    {
        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentBackgroundSpeedIndex < timesToNextBGSpeedUps.Length && currentTime >= timesToNextBGSpeedUps[currentBackgroundSpeedIndex])
            {
                ChangeBackgroundSpeed(speedIncreasePerBGUp[currentBackgroundSpeedIndex]);
                currentBackgroundSpeedIndex++;
            }
            //If the player isnt hitting an obstacle, reduce their distance
            if(((int)currentTime) % 2 == 0)
            {
                eC.StartChangeDistanceCoroutine(2);
            }
            yield return null;
        }
    }
    public void ChangeBackgroundSpeed(int speed)
    {
        targetBackgroundSpeed = Mathf.Clamp(targetBackgroundSpeed + speed, MinSpeed, MaxSpeed);
        print(targetBackgroundSpeed + " speed: " + speed);
    }
    public IEnumerator SpeedChanger()
    {
        while(targetBackgroundSpeed > currentBackgroundSpeed)
        {
            currentBackgroundSpeed = Mathf.Clamp(currentBackgroundSpeed + backgroundSpeedPerStep, MinSpeed, MaxSpeed);
            print(currentBackgroundSpeed);
            yield return new WaitForSeconds(0.25f);
        }
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
        
    }
}
