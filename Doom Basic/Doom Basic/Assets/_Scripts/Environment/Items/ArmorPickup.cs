using UnityEngine;

public class ArmorPickup : MonoBehaviour
{
    [Header("Valor de Armadura")]
    public int armorValue = 10;

    [Header("Efectos opcionales")]
    public AudioClip pickupSound;
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ArmorSystem.Instance != null)
            {
                ArmorSystem.Instance.AddArmor(armorValue);

                // Reproduce sonido y efecto si existen
                if (pickupSound != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                if (pickupEffect != null)
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);

                Destroy(gameObject);
            }
        }
    }
}
