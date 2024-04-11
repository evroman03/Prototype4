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
    public GameObject MoveTowards;
    public GameObject SpawnPoint;
    public RepeatingBackground[] Backgrounds;
    private RepeatingBackground temp;

    public void Start()
    {
        Application.targetFrameRate = 60;
        SpawnBackground(true);
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeBackgroundSpeed(5);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            print("HERE");
            ChangeBackgroundSpeed(-5);
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
            var obj2 = Instantiate(Backgrounds[0].gameObject, SpawnPoint.transform.position, Quaternion.identity);
            temp = obj2.GetComponent<RepeatingBackground>() ;
        }
        else if (!start)
        {

            var obj = Instantiate(Backgrounds[UnityEngine.Random.Range(0, Backgrounds.Length)], temp.spawnTo.position, Quaternion.identity);
            temp = obj;
        }
    }
}
