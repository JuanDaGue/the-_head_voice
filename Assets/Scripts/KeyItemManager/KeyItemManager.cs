using System.Collections.Generic;
using UnityEngine;

public class KeyItemManager : MonoBehaviour
{


    [System.Serializable]
    public struct ItemPrefabEntry
    {
        public KeySpot.KeyItemType type;
        public GameObject prefab;
    }
    [Header("Key Spots")]
    [Tooltip("List of key spots that can be activated by the player.")]
    public List<KeySpot> spots = new List<KeySpot>();
    public float pressTime = 1.0f;
    private float holdTime = 0f;
    private bool isPressing = false;
    [SerializeField] private KeySpot currentSpot;
    
    private GunManager gunManager;
    [Header("Configuración de Prefabs")]
    public ItemPrefabEntry[] prefabEntries;

    public Dictionary<KeySpot.KeyItemType, GameObject> itemPrefabs = new Dictionary<KeySpot.KeyItemType, GameObject>();

    void Awake()
    {

        // Crear y poblar el diccionario
        itemPrefabs = new Dictionary<KeySpot.KeyItemType, GameObject>();
        foreach (var entry in prefabEntries)
        {
            if (entry.prefab != null && !itemPrefabs.ContainsKey(entry.type))
                itemPrefabs.Add(entry.type, entry.prefab);
        }
    }


    void Start()
    {
        gunManager = FindFirstObjectByType<GunManager>();
        
        foreach (KeySpot spot in spots)
        {
            if (spot == null) continue;
            spot.Initialize(this, gunManager);
        }
    }

    void Update()
    {
        if (currentSpot == null) return;
        HandleInput();
    }

    private void HandleInput()
    {
        if (!currentSpot.isActivated)
            return;

        if (Input.GetKey(KeyCode.E))
        {
            if (!isPressing)
            {
                isPressing = true;
                holdTime = 0f;
            }

            holdTime += Time.deltaTime;

            if (holdTime >= pressTime)
            {
                InteractWithCurrentSpot();
                Debug.Log("✅ Activated KeySpot: " + currentSpot.name);
                
                currentSpot.SpawnItem();
                currentSpot.isActivated = false;
                //currentSpot = null;
                Destroy(currentSpot.gameObject);
                isPressing = false;
                holdTime = 0f;
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isPressing = false;
            holdTime = 0f;
        }
    }

    private void InteractWithCurrentSpot()
    {
        Debug.Log("Interacting with KeySpot: " + currentSpot.name);
    }

    public void ActivateSpot(KeySpot spot)
    {
        if (currentSpot != null)
        {
            currentSpot.isActivated = false;
        }
        currentSpot = spot;
        currentSpot.isActivated = true;
    }
    
    public void DeactivateSpot()
    {
        if (currentSpot != null)
        {
            currentSpot.isActivated = false;
        }
    }
}