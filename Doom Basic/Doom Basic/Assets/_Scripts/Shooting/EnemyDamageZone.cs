using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDamageZone : MonoBehaviour
{
    [Header("Configuración de Daño")]
    public float damageAmount = 10f;
    public float damageRate = 1f; // daño cada X segundos
    private float nextDamageTime;

    [Header("Referencias")]
    public Enemy enemy; // referencia al enemigo dueño del collider

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;

        if (enemy == null)
            enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Solo aplica daño si toca al jugador real, no a detectores u otros triggers
        if (other.CompareTag("Player") && !other.isTrigger && Time.time >= nextDamageTime)
        {
            // Ahora pasamos el daño primero por la armadura
            if (ArmorSystem.Instance != null)
            {
                ArmorSystem.Instance.TakeDamage(damageAmount);
                Debug.Log($"🗡 Daño aplicado al jugador: {damageAmount}");
            }
            else
            {
                // Si no hay sistema de armadura (por seguridad)
                var playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    playerHealth.TakeDamage(damageAmount);
            }

            nextDamageTime = Time.time + damageRate;
        }
    }
}
