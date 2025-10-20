using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [Header("Llaves en la interfaz")]
    public Image redKey;
    public Image yellowKey;
    public Image blueKey;

    [Header("Texto temporal en HUD")]
    public TextMeshProUGUI pickupMessageText;
    private Coroutine messageCoroutine;

    // === NUEVO HUD PARA VIDA Y ARMADURA ===
    [Header("HUD de Vida y Armadura")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;

    // === HUD DE ARMA ===
    [Header("HUD de Arma")]
    public Image weaponIcon;
    public TextMeshProUGUI ammoText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        redKey.gameObject.SetActive(false);
        yellowKey.gameObject.SetActive(false);
        blueKey.gameObject.SetActive(false);

        if (pickupMessageText != null)
            pickupMessageText.text = "";

        // Inicializa HUD
        UpdateHealth();
        UpdateArmor();
    }

    void Update()
    {
        // Actualiza en tiempo real
        UpdateHealth();
        UpdateArmor();
    }

    // === MÉTODOS NUEVOS ===
    public void UpdateHealth()
    {
        if (healthText != null && PlayerHealth.Instance != null)
            healthText.text = $"{Mathf.RoundToInt(PlayerHealth.Instance.currentHealth)}";
    }

    public void UpdateArmor()
    {
        if (armorText != null && ArmorSystem.Instance != null)
            armorText.text = $"{ArmorSystem.Instance.currentArmor}";
    }

    public void UpdateWeaponHUD(Sprite icon, int currentAmmo, int totalAmmo)
    {
        if (weaponIcon != null)
            weaponIcon.sprite = icon;

        if (ammoText != null)
            ammoText.text = $"{currentAmmo} / {totalAmmo}";
    }

    // === Métodos existentes (no borres esto) ===
    public void ActivateKey(int key)
    {
        switch (key)
        {
            case 0: redKey.gameObject.SetActive(true); break;
            case 1: yellowKey.gameObject.SetActive(true); break;
            case 2: blueKey.gameObject.SetActive(true); break;
        }
    }

    public void ShowPickupMessage(string message, float duration = 4f)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(ShowMessageRoutine(message, duration));
    }

    private IEnumerator ShowMessageRoutine(string message, float duration)
    {
        pickupMessageText.text = message;
        yield return new WaitForSeconds(duration);
        pickupMessageText.text = "";
        messageCoroutine = null;
    }
}
