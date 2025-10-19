using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // === Singleton ===
    public static PlayerHealth Instance { get; private set; }

    [Header("Configuración de Vida")]
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;

    private void Awake()
    {
        // Patrón Singleton seguro
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Recibe daño directo a la salud (sin pasar por armadura).
    /// </summary>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log($"Jugador recibió daño: {amount}. Vida actual: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }

        // Aquí luego puedes actualizar la UI:
        // UIManager.Instance.UpdateHealthBar(currentHealth / maxHealth);
    }

    /// <summary>
    /// Cura al jugador hasta el máximo permitido.
    /// </summary>
    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"Jugador curado: +{amount}. Vida actual: {currentHealth}");
    }

    private void Die()
    {
        Debug.Log("💀 Jugador ha muerto.");
        // Aquí puedes poner animación de muerte, reinicio, etc.
    }
}
