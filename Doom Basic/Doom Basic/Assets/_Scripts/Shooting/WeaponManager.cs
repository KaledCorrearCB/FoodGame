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

    private int currentWeaponIndex = 0;

    void Start()
    {
        if (availableWeapons.Count > 0)
        {
            EquipWeapon(0); // Equipamos la primera al inicio
        }
    }

    void Update()
    {
        // Cambio con teclas numéricas
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EquipWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) EquipWeapon(4);
        // Agrega más si necesitas
    }

    void EquipWeapon(int index)
    {
        if (index >= 0 && index < availableWeapons.Count)
        {
            currentWeaponIndex = index;
            Guns newGun = availableWeapons[currentWeaponIndex];

            // Asignamos el arma al controlador de disparo
            gunController.gun = newGun;
            gunController.SetTriggers();

            // Mostramos en pantalla el nombre del arma
            weaponNameText.text = "Arma actual: " + newGun.name;
            // o newGun.GunName si agregaste el campo en tu ScriptableObject
        }
    }
}
