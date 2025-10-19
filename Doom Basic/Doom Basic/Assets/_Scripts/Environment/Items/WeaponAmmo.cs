using UnityEngine;

[System.Serializable]
public class WeaponAmmo
{
    public Guns gun;
    public int currentAmmo;
    public int totalAmmo;

    public WeaponAmmo(Guns weapon)
    {
        gun = weapon;
        currentAmmo = weapon.MagazineSize;
        totalAmmo = weapon.MaxAmmo;
    }

    // Método para recargar
    public bool CanReload()
    {
        return currentAmmo < gun.MagazineSize && totalAmmo > 0;
    }

    // Método para realizar la recarga
    public void Reload()
    {
        int ammoNeeded = gun.MagazineSize - currentAmmo;
        int ammoToAdd = Mathf.Min(ammoNeeded, totalAmmo);

        currentAmmo += ammoToAdd;
        totalAmmo -= ammoToAdd;
    }
}