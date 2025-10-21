using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡Ganaste! Cargando escena Winner...");
            SceneManager.LoadScene("Winner");
        }
    }
}
