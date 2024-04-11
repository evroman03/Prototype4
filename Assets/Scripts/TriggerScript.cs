using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public RepeatingBackground rb;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spawn"))
        {
            GameController.Instance.SpawnBackground(false);
        }
    }
}
