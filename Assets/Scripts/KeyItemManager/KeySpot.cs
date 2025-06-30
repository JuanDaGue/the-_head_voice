using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeySpot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject keyItemPrefab;

    public GameObject keyItemUI;

    public bool isActivated = false;

    private Transform spawnPoint;

    private KeyItemManager keyItemManager;
    
    public void Initializate(KeyItemManager manager)
    {
        keyItemManager = manager;
    }

    void Start()
    {
        spawnPoint = this.transform;
        if (keyItemUI != null)
        {
            keyItemUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ShowPickupUIMesage();
            isActivated = true;
            keyItemManager.AtiveSpot(this);
            if (keyItemPrefab != null)
            {
                GameObject keyItem = Instantiate(keyItemPrefab, spawnPoint.position, spawnPoint.rotation);
                keyItem.transform.parent = spawnPoint;
                keyItem.transform.localPosition = Vector3.zero;
                keyItem.transform.localRotation = Quaternion.identity;
            }
            else
            {
                Debug.LogWarning("No key item prefab found!");
            }
        }
    }
    private void ShowPickupUIMesage()
    {
        Debug.Log("Press Space to pick up the key item.");
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HidePickupUIMesage();
            isActivated = false;
            keyItemManager.DeactivateSpot();
        }
    }

    private void HidePickupUIMesage()
    {
        Debug.Log("Key item pickup UI hidden.");
    }
}
