
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Header crea un t�tulo en la variable y el tooltip es una explicaci�n al poner el mouse

    [Header("Animator"), Tooltip("Animator que va a controlar la animaci�n")]
    public Animator animator;

    [Header("Llave"), Tooltip("La llave que abre la puerta")]
    public GameObject key;

    private void OnTriggerEnter(Collider other)
    {
        // Si lo que entra tiene el tag de player y la llave no est� activa
        if (other.CompareTag("Player") && !key.activeSelf)
        {
            animator.SetBool("isOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !key.activeSelf)
        {
            animator.SetBool("isOpen", false);
        }
    }
}
