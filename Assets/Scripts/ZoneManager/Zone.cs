using UnityEngine;

[CreateAssetMenu(menuName = "ZoneConfig/Zone", fileName = "newZone")]

public class Zone : ScriptableObject
{
    [Header("Zone Settings")]
    public string zoneName;
    public Color gizmoColor = Color.cyan; // Color for Gizmos in the editor

    [Header("Zone Dimensions")]
    public Vector2Int size; // Width and height in grid cells
    public Vector2Int offset; // Offset from the origin

    [Header("Enemy Settings")]
    public int maxEnemire = 30; // Number of enemies in the zone
    public float healtMiltiplier = 1.0f; // Multiplier for enemy health
    public float damageMultiplier = 1.0f; // Multiplier for enemy damage

    [Header("Zone Difficulty")]
    public float enemySpawnRate = 1.0f; // Rate at which enemies spawn in the zone
    public float maxTime = 300f; // Maximum time allowed in the zone
    
    public int difficultyLevel = 1; // Difficulty level of the zone
    [Header("Zone UI text")]
    public string startMessages = "Zone Started."; // Description of the zone for UI
    public string progessMessages = "Enemie restants: {0}" ; // Description of the zone for UI
    public string endMessages = "Zone Ended."; // Description of the zone for UI
    public string failMessages = "Zone Failed."; // Description of the zone for UI
    public string  completeMessage= "Zone Completed"; // Message to display when the zone is completed    
}
