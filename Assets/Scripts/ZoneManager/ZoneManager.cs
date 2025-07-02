using UnityEngine;
using System.Collections.Generic;

public class ZoneManager : MonoBehaviour
{
    [Header("Zones")]
    public List<ZoneTrigger> zonesMap = new List<ZoneTrigger>();
    private ZoneTrigger activeZone;
    public ZoneUIManager zoneUIManager; // Add this line
    [Header("Enemy Manager")]
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
        int totalEnemies = zoneConfig.totalEnemies;

        // Show UI
        zoneUIManager.ShowZoneUI(zoneConfig);
        zoneUIManager.UpdateTimer(zoneTimer);
        zoneUIManager.UpdateEnemyCount(totalEnemies - enemiesRemaining, totalEnemies);
        zoneUIManager.ShowStatusMessage(zoneConfig.startMessages, false);

        // Configure enemies
        enemyManager.ConfigureForZone(zoneConfig);
        enemyManager.StartSpawning();
    }
    public void DeactivateZone(ZoneTrigger zone)
    {
        if (activeZone != zone || !isZoneActive) return;

        enemyManager.StopSpawning();
        CleanupEnemies();
        isZoneActive = false;

        Debug.Log(activeZone.currentZone.failMessages);
        Invoke(nameof(HideZoneUI), 3f);
    }
    private void HideZoneUI()
    {
        zoneUIManager.HideZoneUI();
    }
void Update()
    {
        if (!isZoneActive) return;

        // Update timer
        zoneTimer -= Time.deltaTime;
        zoneUIManager.UpdateTimer(zoneTimer);
        
        // Update UI every second to reduce overhead
        if (Time.frameCount % 30 == 0)
        {
            zoneUIManager.UpdateEnemyCount(
                activeZone.currentZone.totalEnemies - enemiesRemaining, 
                activeZone.currentZone.totalEnemies
            );
            
            // Show progress message
            zoneUIManager.ShowProgressMessage(
                string.Format(activeZone.currentZone.progessMessages, enemiesRemaining)
            );
        }

        // Check for failure
        if (zoneTimer <= 0)
        {
            zoneUIManager.ShowStatusMessage(activeZone.currentZone.failMessages, false);
            DeactivateZone(activeZone);
            return;
        }

        // Check for completion
        if (enemiesRemaining <= 0)
        {
            zoneUIManager.ShowStatusMessage(activeZone.currentZone.completeMessage, true);
            DeactivateZone(activeZone);
        }
    }
    public void OnEnemyDefeated()
    {
        if (!isZoneActive) return;
        
        enemiesRemaining--;
        
        // Update UI immediately when enemy is defeated
        zoneUIManager.UpdateEnemyCount(
            activeZone.currentZone.totalEnemies - enemiesRemaining, 
            activeZone.currentZone.totalEnemies
        );
        
        // Show progress message
        zoneUIManager.ShowProgressMessage(
            string.Format(activeZone.currentZone.progessMessages, enemiesRemaining)
        );
    }



    private void CleanupEnemies()
    {
        enemyManager.ClearAllEnemies();
    }
}