using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int currentSnap;
    public PlayerInput playerInput;
    private InputAction left, right;
    private GameObject player;
    private LaneManager LM;
    public Animator carAnimator;
    public bool canTurn = true;

    #region Singleton
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType(typeof(PlayerController)) as PlayerController;
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion
    public void GameReady()
    {
        ControlPanel(true);
        LM = LaneManager.Instance;
        currentSnap = LM.PlayerCenterSnap;
        player = GameController.Instance.Player;
    }
  
    public int CurrentSnap()
    {
        return (currentSnap);
    }
    public bool CanMoveLeft()
    {
        return (currentSnap != 0);
    }
    public bool CanMoveRight()
    {
        return (currentSnap != LM.PlayerSnaps.Length-1);
    }
    public void MoveLeft()
    {
        if (CanMoveLeft() && canTurn)
        {
            //currentSnap -= 1;
            //player.transform.position = LM.PlayerSnaps[currentSnap].transform.position;
            canTurn = false;
            carAnimator.SetTrigger("TurnedLeft");
        }
    }
    public void MoveRight()
    {
        if(CanMoveRight() && canTurn)
        {
            //currentSnap += 1;
            //player.transform.position = LM.PlayerSnaps[currentSnap].transform.position;
            canTurn = false;
            carAnimator.SetTrigger("TurnedRight");
        }
    }
    public void AnimDoneLeft()
    {
        currentSnap -= 1;
        player.transform.position = LM.PlayerSnaps[currentSnap].transform.position;
        canTurn = true;
    }
    public void AnimDoneRight()
    {
        currentSnap += 1;
        player.transform.position = LM.PlayerSnaps[currentSnap].transform.position;
        canTurn = true;
    }









    public void ControlPanel(bool gameStart)
    {
        if (gameStart)
        {
            left = playerInput.currentActionMap.FindAction("Left");
            left.started += ReadMoveLeft;
            left.canceled += EndReadMoveLeft;
            right = playerInput.currentActionMap.FindAction("Right");
            right.started += ReadMoveRight;
            right.canceled += EndReadMoveRight;
        }
        else
        {
            left.started -= ReadMoveLeft;
            left.canceled -= EndReadMoveLeft;
            right.started -= ReadMoveRight;
            right.canceled -= EndReadMoveRight;
        }
    }
    private void ReadMoveLeft(InputAction.CallbackContext ctx)
    {
        MoveLeft();
    }
    private void EndReadMoveLeft(InputAction.CallbackContext ctx)
    {

    }
    private void ReadMoveRight(InputAction.CallbackContext ctx)
    {
        MoveRight();
    }
    private void EndReadMoveRight(InputAction.CallbackContext ctx)
    {

    }
    private void OnDestroy()
    {
        ControlPanel(false);
    }
}
