using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class GunManager : MonoBehaviour
{
    [Header("Weapon Assets")]
    public List<GunBase> guns;

    [Header("Fire Point")]
    public Transform firePoint;

    [Header("Item Inventory")]
    public ItemInventory inventory;

    private Camera playerCamera;
    private LineRenderer lineRenderer;
    private GunBase activeGun;
    private float cooldownTimer = 0f;
    private int ammoInClip = 0;

    void Start()
    {
        playerCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        if (inventory == null)
            inventory = GetComponent<ItemInventory>();

        if (guns.Count > 0)
            EquipGun(0);
    }

    void Update()
    {
        if (activeGun == null) return;

        cooldownTimer -= Time.deltaTime;
        HandleInput();
        SwitchGuns();
    }

    void HandleInput()
    {
        if ((Input.GetKey(KeyCode.E) || Input.GetMouseButton(0)) && cooldownTimer <= 0 && ammoInClip > 0)
        {
            ApplyBuffsToActiveGun();
            activeGun.Fire(firePoint, playerCamera, lineRenderer);
            cooldownTimer = activeGun.fireRate;
            ammoInClip--;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void SwitchGuns()
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (Input.GetKeyDown(guns[i].activationKey))
            {
                EquipGun(i);
                break;
            }
        }
    }

    void EquipGun(int index)
    {
        activeGun = guns[index];
        ammoInClip = activeGun.clipSize;
        cooldownTimer = 0;
        //Debug.Log("Equipped: " + activeGun.weaponName);
    }

    void Reload()
    {
        ammoInClip = activeGun.clipSize;
        //Debug.Log("Reloaded: " + activeGun.weaponName);
    }
    public GunBase GetActiveGun()
    {
        return activeGun;
    }
    public float GetCooldownTime()
    {
        return cooldownTimer;
    }
    public float GetReloadTime()
    {
        return activeGun.fireRate;
    }
    public float GetReloadProgress()
    {
        return (cooldownTimer / activeGun.fireRate);
    }
    public int GetAmmoInClip()
    {
        return ammoInClip;
    }

    void ApplyBuffsToActiveGun()
    {
        //Debug.Log("Applying buffs to active gun...");
        WeaponStats baseStats = new WeaponStats(
            activeGun.baseDamage,
            activeGun.baseFireRate,
            activeGun.baseProjectileSpeed,
            activeGun.baseClipSize

        );

        WeaponStats buffed = inventory.ApplyBuffs(baseStats);

        activeGun.damage = buffed.damage;
        activeGun.fireRate = buffed.fireRate;
        activeGun.projectileSpeed = buffed.projectileSpeed;
        activeGun.clipSize = buffed.clipSize;
    }

    // public int   GetAmmoInClip()     => ammoInClip;
    // public float GetCooldownTime()   => cooldownTimer;
    // public float GetReloadProgress() => cooldownTimer / activeGun.fireRate;
    // public GunBase GetActiveGun()    => activeGun;
}
