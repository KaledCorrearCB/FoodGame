using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [Header("Armas disponibles")]
    public List<Guns> availableWeapons = new List<Guns>();

    [Header("Referencias")]
    public GunController gunController;
    public TMP_Text weaponNameText;
    public TMP_Text ammoText;

    private int currentWeaponIndex = 0;
    private List<WeaponAmmo> weaponsAmmo = new List<WeaponAmmo>();

    public static WeaponManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Inicializar la munición para cada arma
        foreach (Guns weapon in availableWeapons)
        {
            weaponsAmmo.Add(new WeaponAmmo(weapon));
        }

        if (availableWeapons.Count > 0)
        {
            EquipWeapon(0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
    }

    void EquipWeapon(int index)
    {
        if (index >= 0 && index < availableWeapons.Count)
        {
            currentWeaponIndex = index;
            Guns newGun = availableWeapons[currentWeaponIndex];
            WeaponAmmo currentWeaponAmmo = weaponsAmmo[currentWeaponIndex];

            gunController.SetGun(newGun, currentWeaponAmmo);
            weaponNameText.text = "Arma: " + newGun.name;
            UpdateAmmoUI(currentWeaponAmmo);
        }
    }

    private void UpdateAmmoUI(WeaponAmmo weaponAmmo)
    {
        if (ammoText != null && weaponAmmo != null)
        {
            ammoText.text = $"{weaponAmmo.currentAmmo} / {weaponAmmo.totalAmmo}";
        }
    }

    public WeaponAmmo GetCurrentWeaponAmmo()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weaponsAmmo.Count)
        {
            return weaponsAmmo[currentWeaponIndex];
        }
        return null;
    }

    public void AddAmmoToWeapon(int weaponIndex, int amount)
    {
        if (weaponIndex >= 0 && weaponIndex < weaponsAmmo.Count)
        {
            weaponsAmmo[weaponIndex].totalAmmo = Mathf.Min(
                weaponsAmmo[weaponIndex].totalAmmo + amount,
                weaponsAmmo[weaponIndex].gun.MaxAmmo
            );

            if (weaponIndex == currentWeaponIndex)
            {
                UpdateAmmoUI(weaponsAmmo[weaponIndex]);
            }

            Debug.Log($"Munición agregada a {weaponsAmmo[weaponIndex].gun.name}: +{amount}");
        }
    }

    public void AddAmmoByType(AmmoType ammoType, int amount)
    {
        bool ammoAdded = false;

        for (int i = 0; i < availableWeapons.Count; i++)
        {
            if (availableWeapons[i].AmmoType == ammoType)
            {
                AddAmmoToWeapon(i, amount);
                ammoAdded = true;
            }
        }

        if (!ammoAdded)
        {
            Debug.Log($"No se encontraron armas que usen munición {ammoType}");
        }
    }

    public int GetTotalAmmoByType( AmmoType ammoType)
    {
        int total = 0;

        for (int i = 0; i < weaponsAmmo.Count; i++)
        {
            if (availableWeapons[i].AmmoType == ammoType)
            {
                total += weaponsAmmo[i].totalAmmo;
            }
        }

        return total;
    }
}