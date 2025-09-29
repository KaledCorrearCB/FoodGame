
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyName;

    [Tooltip("0 para rojo, 1 para amarillo y 2 para azul")]
    public int colorOfKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HUDManager.instance.ActivateKey(colorOfKey);

            // 👇 mostrar mensaje en pantalla
            HUDManager.instance.ShowPickupMessage("Has recogido: " + keyName);

            gameObject.SetActive(false);
        }
    }
}
