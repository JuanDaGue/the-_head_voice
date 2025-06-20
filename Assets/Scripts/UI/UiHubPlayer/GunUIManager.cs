using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class GunUIManager : MonoBehaviour
{
    [Header("Gun Display")]
    public Image weaponIcon;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI reloadTimeText;
    public TextMeshProUGUI cooldownText;
    public Image cooldownRadial;

    [Header("Ammo")]
    public TextMeshProUGUI ammoText;
    public Slider reloadSlider;

    [Header("Weapon Selection")]
    public List<Button> weaponButtons;
    private GunManager gunManager;

    private ItemInventory inventory;
    [Header("Player Life System")]
    public LifeSystem lifeSystemPlayer;
    public Image lifeBar;


    void Start()
    {
        gunManager = FindFirstObjectByType<GunManager>();
        inventory = FindFirstObjectByType<ItemInventory>();
        if (inventory.equippedItems.Count != 0) SetupWeaponButtons();
    }

    void Update()
    {
        UpdateGunUI();
        UpdateHealthBar();
        SetupWeaponButtons();
    }
void SetupWeaponButtons()
{
    int count = Mathf.Min(weaponButtons.Count, inventory.equippedItems.Count);
    for (int i = 0; i < count; i++)
    {
        int index = i;
        var btn = weaponButtons[i];
        var item = inventory.equippedItems[i];

        //btn.onClick.AddListener(() => gunManager.EquipGun(index));
        btn.GetComponentInChildren<Image>().color = new Color32(85, 85, 85, 255);
        btn.GetComponentInChildren<Image>().sprite = item.itemIcon;
        var label = btn.GetComponentInChildren<TextMeshProUGUI>();
        label.text = item.itemName;
        label.fontSize = 24;
        label.alignment = TextAlignmentOptions.Center;
        btn.gameObject.SetActive(true);
    }

    // Desactiva los botones sobrantes (si hay más botones que armas)
    for (int j = count; j < weaponButtons.Count; j++)
        weaponButtons[j].gameObject.SetActive(false);
}

    void UpdateGunUI()
    {
        var gun = gunManager.GetActiveGun();
        if (gun == null) return;

        weaponName.text = gun.weaponName;
        fireRateText.text = $"Fire Rate: {gun.fireRate:F2}s";
        reloadTimeText.text = $"Reload Time: {gunManager.GetReloadTime():F1}s";
        cooldownText.text = $"Cooldown: {gunManager.GetCooldownTime():F2}s";
        cooldownRadial.fillAmount = 1 - (gunManager.GetCooldownTime() / gun.fireRate);
        weaponIcon.sprite = gun.weaponIcon;
        weaponIcon.color = gunManager.GetCooldownTime() > 0 ? Color.red : Color.white;

        ammoText.text = $"{gunManager.GetAmmoInClip()} / {gun.clipSize}";
        reloadSlider.value = 1 - gunManager.GetReloadProgress();
    }
    
        void UpdateHealthBar()
    {
        lifeBar.fillAmount = lifeSystemPlayer.Current / lifeSystemPlayer.Max;
    }

}
