
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
    public TextMeshProUGUI pickupMessageText; // Arrastra aquí tu Text en el Canvas
    private Coroutine messageCoroutine;

   
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        redKey.gameObject.SetActive(false);
        yellowKey.gameObject.SetActive(false);
        blueKey.gameObject.SetActive(false);

        if (pickupMessageText != null)
            pickupMessageText.text = ""; // vacío al inicio
    }


    public void ActivateKey(int key)
    {
        switch (key)
        {
            case 0:
                redKey.gameObject.SetActive(true);
                break;
            case 1:
                yellowKey.gameObject.SetActive(true);
                break;
            case 2:
                blueKey.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Muestra un mensaje temporal en pantalla
    /// </summary>
    public void ShowPickupMessage(string message, float duration = 4f)
    {
        // Si ya había un mensaje activo, lo interrumpe
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine);
        }
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
