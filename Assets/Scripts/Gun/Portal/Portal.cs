using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    //public Transform destination; // Assign another portal or target point
    public float minHeightY = 5f;

    void Start()
    {


    }
    void Update(){
        if (transform.position.y < minHeightY)
        {
            Debug.LogWarning("Portal is too low! Adjusting position...");
            transform.position = new Vector3(transform.position.x, minHeightY, transform.position.z);
        }   
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