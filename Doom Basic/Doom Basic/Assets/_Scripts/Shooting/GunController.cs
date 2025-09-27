using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    public static GunController instance;

    // Colisionador para que se indique si al enemigo se le puede dispadar
    private BoxCollider gunTrigger;

    // Variable para enlazar el Scriptable Object del arma
    public Guns gun;

    // Almacena el player input
    private PlayerInput playerInput;

    // Máscaras para configurar el Raycast
    public LayerMask raycastLayerMask;

    // Verifica si se puede disparar o no.
    private bool canFire;

    // Variable para almacenar el tiempo para poder disparar
    private float nextTimeToFire;

    // Almacena el Audio Source para el disparo del arma
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Se enlaza el colisionador a la variable
        gunTrigger = GetComponent<BoxCollider>();
        SetTriggers();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        canFire = true;
     }

    // Cooldown del arma
    IEnumerator CanFire(float time)
    {
        canFire = false;

        yield return new WaitForSeconds(time);

        canFire = true;

    }
    // Inicializa los triggers de acuerdo al arma
    public void SetTriggers()
    {
        // Se configura el tamaño y la posición del colisionador
        gunTrigger.size = new Vector3(gun.HorizontalRange, gun.VerticalRange, gun.Range);

        // Se ubica el centro de la caja para, a partir de ah[i, generarla
        gunTrigger.center = new Vector3(0, (0.5f * gun.VerticalRange - 1f), gun.Range * 0.5f);
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && canFire)
        {

            audioSource.PlayOneShot(gun.Sound);

            // Se recorre la lista de los enemigos
            foreach (Enemy enemy in EnemyManager.instance.enemiesInRange)
            {
                // Se crea un Raycast para verificar que no se atraviesen paredes

                // Se obtiene la dirección entre dos elementos, para saber a donde enviar el rayo
                var dir = (enemy.transform.position - transform.position).normalized;

                // Se almacena la info del elemento con el que choca el Raycast
                RaycastHit hit;

                ///Se crea un rayo que inicia en el jugador, se dirige en la dirección, tiene la distancia del rango del arma con un poco más
                /// para que llegue a la esquina, y se pone la máscara que ignora
                /// Out saca info de lo primero que toca y lo almacena en la variable.
                /// raycastLayerMask ignora los elementos que no están en esa capa, por eso se puso el box del arma en una nueva layer

                if (Physics.Raycast(transform.position, dir, out hit, gun.Range * 1.5f, raycastLayerMask))
                {
                    // Si lo que toca el Raycast es un enemigo, le hace daño
                    if (hit.transform == enemy.transform) 
                    {
                        // Daño al enemigo que tocó el rayo
                        enemy.Damage(gun.Damage);
                    }
                }
                
                // Visualmente se ve así
                Debug.DrawRay(transform.position, dir * gun.Range, Color.green, 1f);
            }

            StartCoroutine(CanFire(gun.FireRate));
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sirve Collider");
        // Si lo que entra en el colisionador trigger del arma es un enemigo, se adiciona uno a la lista.
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            EnemyManager.instance.AddEnemy(enemy);
        }
    }

    // Si lo que sale del colisionador trigger del arma es un enemigo, se remueve uno a la lista.
    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)
        {
            EnemyManager.instance.RemoveEnemy(enemy);
        }
    }

}
