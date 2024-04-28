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

    [HideInInspector] public int currentBackgroundSpeedIndex = 0;
    [HideInInspector] public float targetBGSpeed;
    public float MaxSpeed = 50, MinSpeed = 30f, currentTime, BGSpeedPerStep, currentBGSpeed;

    //These should probably just be removed and speed should constantly increase but we are lacking time
    public float[] timesToNextBGSpeedUps;
    public int[] speedIncreasePerBGUp;

    public GameObject Player, Enemy;
    public GameObject MoveTowards, BackgroundSpawn;

    public RepeatingBackground[] Backgrounds;

    private EnemyCarController eC;
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
       
        Player.transform.position = LaneManager.Instance.PlayerSnaps[LaneManager.Instance.PlayerCenterSnap].transform.position;
        Enemy.transform.position = LaneManager.Instance.EnemySnaps[LaneManager.Instance.EnemyCenterSnap].transform.position;
        eC = Enemy.GetComponent<EnemyCarController>();
        Enemy.transform.position = new Vector3(Enemy.transform.position.x, Enemy.transform.position.y, eC.OriginDistFromPlayer);
        eC.GameReady();

        StartCoroutine(ObstacleTimer());
        StartCoroutine(SpeedChanger());
        currentBGSpeed = MinSpeed;
        targetBGSpeed = MinSpeed;
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
            //If the player isnt hitting an obstacle, reduce their distance to the enemy
            if(((int)currentTime) % 2 == 0)
            {
                //eC.StartChangeDistanceCoroutine(2);
            }
            yield return null;
        }
    }
    public void ChangeBackgroundSpeed(int speed)
    {
        targetBGSpeed = Mathf.Clamp(targetBGSpeed + speed, MinSpeed, MaxSpeed);
    }
    public IEnumerator SpeedChanger()
    {
        while(true)
        {
            if(targetBGSpeed > currentBGSpeed)
            {
                currentBGSpeed = Mathf.Clamp(currentBGSpeed + BGSpeedPerStep, MinSpeed, MaxSpeed);
            }
            else if (targetBGSpeed < currentBGSpeed)
            {
                currentBGSpeed = Mathf.Clamp(currentBGSpeed - BGSpeedPerStep, MinSpeed, MaxSpeed);
            }
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
}
