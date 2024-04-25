using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimationCaller : MonoBehaviour
{
    public GameObject Car;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnedLeft()
    {
        playerController.AnimDoneLeft();
    }

    public void TurnedRight()
    {
        playerController.AnimDoneRight();
    }
}
