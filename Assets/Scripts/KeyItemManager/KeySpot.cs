using UnityEngine;

public class KeySpot : MonoBehaviour
{
    public enum KeyItemType { Key, Gun, Generic, Collectable }

    [Header("Spot Configuration")]
    public KeyItemType itemType;
    //public GameObject itemPrefab;
    public bool isActivated = false;

    [Header("UI Elements")]
    public GameObject keyItemUI;
    
    private Transform spawnPoint;
    private KeyItemManager keyItemManager;
    private GunManager gunManager;
    
    public void Initialize(KeyItemManager manager, GunManager gunMgr)
    {
        keyItemManager = manager;
        gunManager = gunMgr;
    }

    void Start()
    {
        spawnPoint = this.transform;
        if (keyItemUI != null)
        {
            keyItemUI.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPickupUIMessage();
            isActivated = true;
            keyItemManager.ActivateSpot(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HidePickupUIMessage();
            isActivated = false;
            keyItemManager.DeactivateSpot();
        }
    }

    public void SpawnItem()
    {
        switch (itemType)
        {
            case KeyItemType.Key:
                if (keyItemManager.itemPrefabs[itemType] != null)
                {
                    Instantiate(keyItemManager.itemPrefabs[itemType], spawnPoint.position, spawnPoint.rotation);
                }
                break;
            case KeyItemType.Collectable:
                if (keyItemManager.itemPrefabs[itemType] != null)
                {
                    Instantiate(keyItemManager.itemPrefabs[itemType], spawnPoint.position, spawnPoint.rotation);
                }
                break;
            case KeyItemType.Generic:
                if (keyItemManager.itemPrefabs[itemType] != null)
                {
                    Instantiate(keyItemManager.itemPrefabs[itemType], spawnPoint.position, spawnPoint.rotation);
                }
                break;
            case KeyItemType.Gun:
                SpawnRandomGun();
                break;
        }
    }

    private void SpawnRandomGun()
    {
        if (gunManager != null && gunManager.allGuns.Count > 0)
        {
            // Select random gun
            int randomIndex = Random.Range(0, gunManager.allGuns.Count);
            GunBase selectedGun = gunManager.allGuns[randomIndex];

            // Add to player's available guns
            if (!gunManager.guns.Contains(selectedGun))
            {
                gunManager.guns.Add(selectedGun);
                gunManager.EquipGun(gunManager.guns.Count - 1);
            }
            

        }
    }

    private void ShowPickupUIMessage()
    {
        if (keyItemUI != null)
        {
            keyItemUI.SetActive(true);
        }
    }

    private void HidePickupUIMessage()
    {
        if (keyItemUI != null)
        {
            keyItemUI.SetActive(false);
        }
    }
}