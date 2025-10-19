using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Header("Configuración de Curación")]
    public float healAmount = 25f; // cantidad de vida que restaura

    [Header("Efectos opcionales")]
    public AudioClip pickupSound;
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Asegúrate de que el sistema de salud exista
            if (PlayerHealth.Instance != null)
            {
                PlayerHealth.Instance.Heal(healAmount);
                Debug.Log($"💖 Jugador recuperó {healAmount} de vida. Vida actual: {PlayerHealth.Instance.currentHealth}");

                // Reproduce sonido si hay uno asignado
                if (pickupSound != null)
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position);

                // Instancia efecto visual si hay uno asignado
                if (pickupEffect != null)
                    Instantiate(pickupEffect, transform.position, Quaternion.identity);

                // Destruye el pickup
                Destroy(gameObject);
            }
        }
    }
}
