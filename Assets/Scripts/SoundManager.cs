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

    [SerializeField] private AudioClip HitRoadBlock;
    [SerializeField] private AudioClip HitTrashBin;
    [SerializeField] private AudioClip HitHotDogCar;
    [SerializeField] private AudioClip HitPedestrian;
    [SerializeField] private AudioClip CoinCollected;
    [SerializeField] private AudioClip GoodBarrelCollected;
    [SerializeField] private AudioClip HitBadBarrel;
    [SerializeField] private AudioClip RollingBarrel;
    [SerializeField] private AudioClip BouncingBarrel;
    [SerializeField] private AudioClip RaiseMultiplier;
    [SerializeField] private AudioClip GameComplete;
    [SerializeField] private AudioClip SwitchingLanes;
    [SerializeField] private AudioClip CarChase;

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
        AudioSource.PlayClipAtPoint(HitRoadBlock, audioLocation.transform.position);
    }

    //called when the player hits a trash bin
    public void TrashHit()
    {
        AudioSource.PlayClipAtPoint(HitTrashBin, audioLocation.transform.position);
    }

    //called when the player hits the hot dog truck
    public void HDCarHit()
    {
        AudioSource.PlayClipAtPoint(HitHotDogCar, audioLocation.transform.position);
    }

    //called when the player hits the pedestrian
    public void PedestrianHit()
    {
        AudioSource.PlayClipAtPoint(HitPedestrian, audioLocation.transform.position);
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
    public void RollingBadBarrel()
    {
        AudioSource.PlayClipAtPoint(RollingBarrel, audioLocation.transform.position);
    }

    //can be called when the good barrel is spawned
    public void BouncingGoodBarrel()
    {
        AudioSource.PlayClipAtPoint(BouncingBarrel, audioLocation.transform.position);
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

}
