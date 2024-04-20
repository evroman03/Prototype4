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
    [SerializeField] private AudioClip RaiseMultiplier;
    [SerializeField] private AudioClip JumpSound;
    [SerializeField] private AudioClip GameComplete;

    [SerializeField] private GameObject audioLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObstacleHit()
    {
        AudioSource.PlayClipAtPoint(HitObstacle, audioLocation.transform.position);
    }

    public void CollectCoin()
    {
        AudioSource.PlayClipAtPoint(CoinCollected, audioLocation.transform.position);
    }

    public void CollectGoodBarrel()
    {
        AudioSource.PlayClipAtPoint(GoodBarrelCollected, audioLocation.transform.position);
    }

    public void BadBarrelHit()
    {
        AudioSource.PlayClipAtPoint(HitBadBarrel, audioLocation.transform.position);
    }

    public void MultiplierRaised()
    {
        AudioSource.PlayClipAtPoint(RaiseMultiplier, audioLocation.transform.position);
    }

    public void JumpNoise()
    {
        AudioSource.PlayClipAtPoint(JumpSound, audioLocation.transform.position);
    }

    public void ChaseCompleted()
    {
        AudioSource.PlayClipAtPoint(GameComplete, audioLocation.transform.position);
    }
}
