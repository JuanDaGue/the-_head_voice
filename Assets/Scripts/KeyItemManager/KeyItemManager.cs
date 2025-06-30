using System;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class KeyItemManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    public List<KeySpot> spots = new List<KeySpot>();
    private float pressTime = 1.0f; // Time to hold the key to activate
    private float holdTime = 0f; // Time the key has been held down
    private bool isPressing = false;
    [SerializeField] private KeySpot currentSpot;
    void Start()
    {
        Debug.Log("Current Spot: " + currentSpot.name);
        foreach (KeySpot spot in spots)
        {
            if (spot == null) continue; // Skip null spots
            spot.Initializate(this);
        }
        //currentSpot.Initializate(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpot == null) return;
        HandleInput();
    }

private void HandleInput()
{
    if (currentSpot == null || !currentSpot.isActivated)
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

            Destroy(currentSpot.gameObject);
            currentSpot.isActivated = false;
            currentSpot = null;

            // Reset pressing state
            isPressing = false;
            holdTime = 0f;
        }
    }

    // Reset if key is released before the required time
    if (Input.GetKeyUp(KeyCode.E))
    {
        if (holdTime < pressTime)
        {
            Debug.Log("⏱️ Key released too early. Held for " + holdTime.ToString("F2") + " seconds.");
        }

        isPressing = false;
        holdTime = 0f;
    }
}
    private void InteractWithCurrentSpot()
    {
        Debug.Log("Interacting with KeySpot: " + currentSpot.name);
    }

    public enum KeyItemType
    {
        key,
        Gun,
        Generic,
        Collectable,

    }

    public void AtiveSpot(KeySpot spot)
    {
        if (currentSpot != null)
        {
            currentSpot.isActivated = false; // Deactivate the previous spot
        }
        currentSpot = spot;
        currentSpot.isActivated = true; // Activate the new spot
        Debug.Log("Activated KeySpot: " + currentSpot.name);
    }
    public void DeactivateSpot()
    {
        if (currentSpot != null)
        {
            currentSpot.isActivated = false; // Deactivate the previous spot
        }
        //currentSpot = null;
    }
}
