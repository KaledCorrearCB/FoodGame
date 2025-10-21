using UnityEngine;
using UnityEngine.SceneManagement;

public class AcidDamage : MonoBehaviour
{
    [Header("Configuración del ácido")]
    [Tooltip("Daño por segundo si el jugador no tiene armadura.")]
    public float acidDamage = 9999f; // daño letal
    public bool instantKill = true;  // mata instantáneamente

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerArmor armor = other.GetComponent<PlayerArmor>();
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            if (armor != null && armor.HasArmor)
            {
                Debug.Log("🛡️ El jugador tiene armadura, el ácido no le afecta.");
                return;
            }

            if (health != null)
            {
                if (instantKill)
                {
                    health.TakeDamage(health.maxHealth); // mata de una
                }
                else
                {
                    health.TakeDamage(acidDamage);
                }

                // Opción: mandar a escena de Game Over
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
