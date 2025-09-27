
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    // Almacena las imagenes de las llaves en el inventario
    [Header("Llaves en la interfaz")]
    public Image redKey;
    public Image yellowKey;
    public Image blueKey;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        redKey.gameObject.SetActive(false);
        yellowKey.gameObject.SetActive(false);
        blueKey.gameObject.SetActive(false);
    }
    
    // Activa la llave de acuerdo al número
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
}
