using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isActive = false;
    public Zone[] zones; // Reference to the Zone scriptable object or class

    public Zone currentZone; // Reference to the Zone scriptable object or class

    private ZoneManager zoneManager;

    public void Initialized(ZoneManager manager)
    {
        zoneManager = manager;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActive)
        {
            isActive = true;

            SelectRandomZone();

            zoneManager.SetCurrentZone(this);

            // TODO: enemyManager spawn enemies in the current zone
        }
    }
    
    private void SelectRandomZone()
    {
        if (zones.Length == 0) return;

        // Select a random zone from the array
        int randomIndex = Random.Range(0, zones.Length);
        currentZone = zones[randomIndex];
    }


    private void OnDrawGizmos()
    {
        // if (zone != null)
        // {
        //     Gizmos.color = zone.gizmoColor; // Assuming Zone has a color property
        //     Gizmos.DrawWireCube(transform.position, transform.localScale); // Assuming Zone has a size property
        // }
    }

}
