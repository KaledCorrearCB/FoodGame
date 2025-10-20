using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GunController : MonoBehaviour
{
    //Autoload  singleton
    public static GunController instance;

    private BoxCollider gunTrigger;
    public Guns gun;
    private PlayerInput playerInput;
    public LayerMask raycastLayerMask;
    private bool canFire;
    private float nextTimeToFire;
    public AudioSource audioSource;
    //Modelo y balas
    private WeaponAmmo currentWeaponAmmo;
    private bool isReloading = false;
    private WeaponManager weaponManager;
    private GameObject currentGunModel;
    //Punto donde debe aparecer el arma
    [Header("Punto donde se coloca el modelo del arma")]
    [SerializeField] private Transform gunModelParent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        gunTrigger = GetComponent<BoxCollider>();
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        canFire = true;
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    IEnumerator Reload()
    {
        if (isReloading || currentWeaponAmmo == null || !currentWeaponAmmo.CanReload())
            yield break;

        isReloading = true;
        canFire = false;

        Debug.Log("Recargando...");

        yield return new WaitForSeconds(gun.ReloadTime);

        currentWeaponAmmo.Reload();

        isReloading = false;
        canFire = true;

        if (weaponManager != null)
        {
           // weaponManager.UpdateAmmoUI(currentWeaponAmmo);
        }

        Debug.Log("Recarga completada");
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && canFire && !isReloading && currentWeaponAmmo != null)
        {
            if (currentWeaponAmmo.currentAmmo <= 0)
            {
                if (currentWeaponAmmo.totalAmmo > 0)
                {
                    StartCoroutine(Reload());
                }
                else
                {
                    Debug.Log("¡Sin munición!");
                }
                return;
            }

            currentWeaponAmmo.currentAmmo--;

            if (weaponManager != null)
            {
               //  weaponManager.UpdateAmmoUI(currentWeaponAmmo);
            }

            audioSource.PlayOneShot(gun.Sound);

            if (EnemyManager.instance != null)
            {
               // EnemyManager.instance.CleanNullEnemies();
            }

            if (EnemyManager.instance != null && EnemyManager.instance.enemiesInRange.Count > 0)
            {
                foreach (Enemy enemy in EnemyManager.instance.enemiesInRange)
                {
                    if (enemy == null) continue;

                    var dir = (enemy.transform.position - transform.position).normalized;
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position, dir, out hit, gun.Range * 1.5f, raycastLayerMask))
                    {
                        if (hit.transform == enemy.transform)
                        {
                            enemy.Damage(gun.Damage);
                        }
                    }

                    Debug.DrawRay(transform.position, dir * gun.Range, Color.green, 1f);
                }
            }

            StartCoroutine(CanFire(gun.FireRate));

            if (currentWeaponAmmo.currentAmmo <= 0 && currentWeaponAmmo.totalAmmo > 0)
            {
                StartCoroutine(Reload());
            }
        }
    }

    public void SetGun(Guns newGun, WeaponAmmo weaponAmmo)
    {
        gun = newGun;
        currentWeaponAmmo = weaponAmmo;
        SetTriggers();

        Debug.Log($"Arma cambiada: {gun.name}, Munición: {weaponAmmo.currentAmmo}/{weaponAmmo.totalAmmo}");

        if (currentGunModel != null)
        {
            Destroy(currentGunModel);
        }

        if (gun.GunModelPrefab != null)
        {
            // Si se definió un punto de anclaje, usarlo
            Transform parent = gunModelParent != null ? gunModelParent : transform;

            currentGunModel = Instantiate(gun.GunModelPrefab, parent);
            currentGunModel.transform.localPosition = Vector3.zero;
            currentGunModel.transform.localRotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning($"El arma {gun.name} no tiene asignado un modelo visual.");
        }
    }

    // [Mantén tus otros métodos existentes...]
    IEnumerator CanFire(float time)
    {
        canFire = false;
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    public void SetTriggers()
    {
        if (gunTrigger != null && gun != null)
        {
            gunTrigger.size = new Vector3(gun.HorizontalRange, gun.VerticalRange, gun.Range);
            gunTrigger.center = new Vector3(0, (0.5f * gun.VerticalRange - 1f), gun.Range * 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy != null && EnemyManager.instance != null)
        {
            EnemyManager.instance.AddEnemy(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();
        if (enemy != null && EnemyManager.instance != null)
        {
            EnemyManager.instance.RemoveEnemy(enemy);
        }
    }
}