using UnityEngine;

public class Shotgun : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DoorActivate"))
        {
            Debug.Log("Shotgun triggered DoorActivate");
            Destroy(other.gameObject); // Destroy the DoorActivate object
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("DoorActivate"))
        {
            Debug.Log("Shotgun collided with DoorActivate");
            Destroy(collision.gameObject); // Destroy the shotgun itself
        }
    }
}