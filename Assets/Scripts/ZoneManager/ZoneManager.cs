using UnityEngine;
using System.Collections.Generic;

public class ZoneManager : MonoBehaviour
{
    public List<ZoneTrigger> zonesMap = new List<ZoneTrigger>();
    private ZoneTrigger activeZone;
    public EnemyManager enemyManager;
    private float zoneTimer;
    private int enemiesRemaining;
    private bool isZoneActive;

    void Start()
    {



        enemyManager.Initialize(this); // Add this line
    
        foreach (ZoneTrigger zoneTrigger in zonesMap)
        {
            zoneTrigger.Initialize(this);
        }
    }

    public void ActivateZone(ZoneTrigger zone)
    {
        if (isZoneActive) return;

        activeZone = zone;
        isZoneActive = true;
        Zone zoneConfig = activeZone.currentZone;

        // Initialize zone
        zoneTimer = zoneConfig.maxTime;
        enemiesRemaining = zoneConfig.totalEnemies;

        // Configure enemies
        enemyManager.ConfigureForZone(zoneConfig);
        enemyManager.StartSpawning();

        // UI and debug
        Debug.Log(zoneConfig.startMessages);
        UpdateProgressUI();
    }

    public void DeactivateZone(ZoneTrigger zone)
    {
        if (activeZone != zone || !isZoneActive) return;
        
        enemyManager.StopSpawning();
        CleanupEnemies();
        isZoneActive = false;
        
        Debug.Log(activeZone.currentZone.failMessages);
    }

    void Update()
    {
        if (!isZoneActive) return;

        // Update timer
        zoneTimer -= Time.deltaTime;
        // Debug.Log($"Time remaining: {zoneTimer:F2} seconds");
        // Debug.Log($"Enemies remaining: {enemiesRemaining}");
        // Check for failure
        if (zoneTimer <= 0)
        {
            Debug.Log("Timer "+ activeZone.currentZone.failMessages);
            DeactivateZone(activeZone);
            return;
        }

        // Check for completion
        if (enemiesRemaining <= 0)
        {
            Debug.Log("Enemies remaining "+ activeZone.currentZone.completeMessage);
            DeactivateZone(activeZone);
        }
    }

    public void OnEnemyDefeated()
    {
        if (!isZoneActive) return;
        
        enemiesRemaining--;
        UpdateProgressUI();
    }

    private void UpdateProgressUI()
    {
        string progress = string.Format(
            activeZone.currentZone.progessMessages, 
            enemiesRemaining
        );
        Debug.Log(progress);
    }

    private void CleanupEnemies()
    {
        enemyManager.ClearAllEnemies();
    }
}