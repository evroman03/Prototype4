using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject moveTowards;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveTowards = GameController.Instance.MoveTowards;
    }
    private void Update()
    {
        var step = GameController.Instance.BackgroundSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(rb.position, moveTowards.transform.position, step);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MoveTowards"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Spawn"))
        {
            GameController.Instance.SpawnBackground(false);
        }
    }
}
