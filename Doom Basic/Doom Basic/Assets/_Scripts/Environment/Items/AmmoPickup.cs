using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Configuración de munición")]
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int ammoAmount = 30;

    [Header("Efectos")]
    [SerializeField] private GameObject pickupEffect;
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Entró en trigger con: {other.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("El objeto es el jugador.");

            WeaponManager weaponManager = WeaponManager.Instance;

            if (weaponManager == null)
            {
                Debug.LogWarning("❌ No se encontró el WeaponManager (asegúrate de que exista en la escena).");
                return;
            }

            int totalBefore = weaponManager.GetTotalAmmoByType(ammoType);
            weaponManager.AddAmmoByType(ammoType, ammoAmount);
            int totalAfter = weaponManager.GetTotalAmmoByType(ammoType);

            if (totalAfter > totalBefore)
            {
                Debug.Log($"✅ Recogiste {ammoAmount} de {ammoType}");

                if (pickupEffect != null)
                    Instantiate(pickupEffect, transform.position, transform.rotation);

                if (pickupSound != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"⚠️ No tienes armas que usen munición {ammoType}");
            }
        }
    }
}
