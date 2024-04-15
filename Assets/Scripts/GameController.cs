using JetBrains.Annotations;
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

    public float MaxSpeed=50, MinSpeed=30f;
    public float BackgroundSpeed;

    public GameObject Player, Enemy;
    public RepeatingBackground[] Backgrounds;


    public GameObject[] Snaps;
    public GameObject[] EnemySnaps;
    public GameObject MoveTowards, BackgroundSpawn;


    [HideInInspector] public int PlayerCenterSnap, EnemyCenterSnap;
    private RepeatingBackground temp;



    public void Start()
    {
        Application.targetFrameRate = 60;
        SpawnBackground(true);
        PlayerCenterSnap = Snaps.Length / 2;
        EnemyCenterSnap = EnemySnaps.Length/2;
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Player = Instantiate(Player);
        }
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Enemy = Instantiate(Enemy);
            Enemy.GetComponentInChildren<EnemyCarController>().GameReady();
        }
        Player.transform.position = Snaps[PlayerCenterSnap].transform.position;
        Enemy.transform.position = EnemySnaps[EnemyCenterSnap].transform.position;
        PlayerController.Instance.GameReady();
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
        if(Input.GetKeyDown(KeyCode.O))
        {
            Enemy.GetComponentInChildren<EnemyCarController>().ChangeDistance(-10f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Enemy.GetComponentInChildren<EnemyCarController>().ChangeDistance(10f);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
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
}
