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

    public float BackgroundSpeed;
    public GameObject MoveTowards;
    public GameObject SpawnPoint;
    public GameObject[] Backgrounds;
    private GameObject temp;

    public void Start()
    {
        Application.targetFrameRate = 60;
        SpawnBackground(true);
    }
    public void Update()
    {
        
    }
    public void SpawnBackground(bool start)
    {
        if (start)
        {
            var obj2 = Instantiate(Backgrounds[UnityEngine.Random.Range(0, Backgrounds.Length)], SpawnPoint.transform.position, Quaternion.identity);
            temp = obj2;
        }
        else if (!start)
        {
            var obj = Instantiate(Backgrounds[UnityEngine.Random.Range(0, Backgrounds.Length)], temp.transform.GetChild(0).transform.position, Quaternion.identity);
            temp = obj;
        }
    }
}
