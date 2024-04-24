using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(SoundManager)) as SoundManager;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    [SerializeField] private AudioClip HitObstacle;
    [SerializeField] private AudioClip CoinCollected;
    [SerializeField] private AudioClip GoodBarrelCollected;
    [SerializeField] private AudioClip HitBadBarrel;
    [SerializeField] private AudioClip RollingBarrel;
    [SerializeField] private AudioClip RaiseMultiplier;
    [SerializeField] private AudioClip GameComplete;
    [SerializeField] private AudioClip SwitchingLanes;
    [SerializeField] private AudioClip CarChase;
    [SerializeField] private AudioClip RevvingEngine;

    [SerializeField] private GameObject audioLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //called when the player hits a road block obstacle
    public void ObstacleHit()
    {
        AudioSource.PlayClipAtPoint(HitObstacle, audioLocation.transform.position);
    }

    //called when the player collects a coin
    public void CollectCoin()
    {
        AudioSource.PlayClipAtPoint(CoinCollected, audioLocation.transform.position);
    }

    //called when the player picks up a good barrel
    public void CollectGoodBarrel()
    {
        AudioSource.PlayClipAtPoint(GoodBarrelCollected, audioLocation.transform.position);
    }

    //called when the player hits a bad barrel
    public void BadBarrelHit()
    {
        AudioSource.PlayClipAtPoint(HitBadBarrel, audioLocation.transform.position);
    }

    //can be called when the bad barrel is spawned
    private void RollTheBarrel()
    {
        AudioSource.PlayClipAtPoint(RollingBarrel, audioLocation.transform.position);
    }

    //called when the score multiplier increases
    public void MultiplierRaised()
    {
        AudioSource.PlayClipAtPoint(RaiseMultiplier, audioLocation.transform.position);
    }

    //called when the countdown timer reaches zero
    public void ChaseCompleted()
    {
        AudioSource.PlayClipAtPoint(GameComplete, audioLocation.transform.position);
    }

    //called when the player switches lanes
    public void LaneSwitch()
    {
        AudioSource.PlayClipAtPoint(SwitchingLanes, audioLocation.transform.position);
    }

    //called when the catch mechanic starts
    public void CarChaseStarted()
    {
        AudioSource.PlayClipAtPoint(CarChase, audioLocation.transform.position);
    }

    //called at the start of the game
    public void StartYourEngines()
    {
        AudioSource.PlayClipAtPoint(RevvingEngine, audioLocation.transform.position);
    }
}
