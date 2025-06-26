using System;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<ZoneTrigger> zonesMap = new List<ZoneTrigger>(); // List of zones to manage
    private ZoneTrigger activeZone; // Currently active zonprivate 
    public EnemyManager enemyManager; // Reference to the EnemyManager
 

    public void SetCurrentZone(ZoneTrigger zone)
    {
        if (zone == null) return;

        // Check if the zone is already active
        if (activeZone != null && activeZone.isActive)
        {
            Debug.Log($"Zone {activeZone.currentZone.name} is already active.");
            return;
        }

        // Set the new active zone
        activeZone = zone;
    }

}
