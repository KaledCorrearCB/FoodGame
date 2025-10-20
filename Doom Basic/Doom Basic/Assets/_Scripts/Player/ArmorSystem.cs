using UnityEngine;

public class ArmorSystem : MonoBehaviour
{
    // === Singleton ===
    public static ArmorSystem Instance { get; private set; }

    [Header("Configuración de Armadura")]
    public int maxArmor = 25;
    [HideInInspector] public int currentArmor;

    [Tooltip("Cantidad que la armadura reduce por golpe recibido.")]
    public int armorReductionPerHit = 2;

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
        currentArmor = maxArmor;
    }

    /// <summary>
    /// Recibe daño y lo distribuye entre armadura y vida.
    /// </summary>
    public void TakeDamage(float amount)
    {
        if (currentArmor > 0)
        {
            // Calcula cuánto absorbe la armadura
            int absorbed = Mathf.Min(armorReductionPerHit, currentArmor);
            currentArmor -= absorbed;

            Debug.Log($"🛡 Armadura absorbió {absorbed}. Armadura restante: {currentArmor}");

            // Lo que no absorba pasa a la vida
            float remainingDamage = amount - absorbed;
            if (remainingDamage > 0 && PlayerHealth.Instance != null)
            {
                PlayerHealth.Instance.TakeDamage(remainingDamage);
            }
        }
        else
        {
            // Sin armadura → daño directo a la vida
            if (PlayerHealth.Instance != null)
                PlayerHealth.Instance.TakeDamage(amount);
        }

        // Luego puedes actualizar la UI:
        // UIManager.Instance.UpdateArmorBar((float)currentArmor / maxArmor);
    }

    /// <summary>
    /// Añade armadura (por recoger un ítem, por ejemplo).
    /// </summary>
    public void AddArmor(int amount)
    {
        currentArmor = Mathf.Clamp(currentArmor + amount, 0, maxArmor);
        Debug.Log($" Jugador recogió {amount} de armadura. Total actual: {currentArmor}");
    }
}
