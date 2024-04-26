using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimationCaller : MonoBehaviour
{
    public GameObject Car;
    public PlayerController playerController;
    Quaternion origRotation;
    // Start is called before the first frame update
    void Start()
    {
        origRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnedLeft()
    {
        playerController.AnimDoneLeft();
        transform.rotation = origRotation;
    }

    public void TurnedRight()
    {
        playerController.AnimDoneRight();
        transform.rotation = origRotation;
    }
}
