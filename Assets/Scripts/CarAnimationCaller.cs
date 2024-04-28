using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimationCaller : MonoBehaviour
{
    public PlayerController playerController;
    Quaternion origRotation;
    // Start is called before the first frame update
    void Start()
    {
        origRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnedLeft()
    {
        ResetAll();
        playerController.AnimDoneLeft();

    }

    public void TurnedRight()
    {
        ResetAll();
        playerController.AnimDoneRight();

    }
    public void ResetAll()
    {
        //transform.rotation = Quaternion.identity;
        //transform.position = new Vector3(0, 0, 0);
        //print("RESETALL");
    }
}
