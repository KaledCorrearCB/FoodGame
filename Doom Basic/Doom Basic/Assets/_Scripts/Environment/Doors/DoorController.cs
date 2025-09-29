using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Animator"), Tooltip("Animator que va a controlar la animación")]
    public Animator animator;

    [Header("Llave"), Tooltip("La llave que abre la puerta")]
    public GameObject key;

    [Header("Audio"), Tooltip("Fuente de audio para reproducir sonidos")]
    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !key.activeSelf)
        {
            animator.SetBool("isOpen", true);
            PlaySound(openSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !key.activeSelf)
        {
            animator.SetBool("isOpen", false);
            PlaySound(closeSound);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
