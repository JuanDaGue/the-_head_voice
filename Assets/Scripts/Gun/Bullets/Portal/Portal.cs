using UnityEngine;

public class Portal : Bullet
{
    [Header("Portal Settings")]
    //public Transform destination; // Assign another portal or target point
    public float minHeightY = 5f;

    protected override void Start()
    {


    }
    void Update(){
        if (transform.position.y < minHeightY)
        {
            //Debug.LogWarning("Portal is too low! Adjusting position...");
            transform.position = new Vector3(transform.position.x, minHeightY, transform.position.z);
        }   
    }

    protected override void OnHit(Collider other)
    {
        // Implement your logic for when the portal is hit
        Debug.Log("Portal OnHit called with: " + other.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Portal triggered by Enemy");
            Destroy(other.gameObject);
        }
    }
}