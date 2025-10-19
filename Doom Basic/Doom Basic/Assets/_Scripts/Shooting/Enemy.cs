
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variable de la vida de los enemigos
    private float health = 2;
    public GameObject gunHit;

    // Guarda los materiales para el cambio
    private Material originalMaterial;
    public Material detectedMaterial;
    private MeshRenderer _meshRenderer;

    private EnemyAI enemyAI;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = _meshRenderer.sharedMaterial;
        enemyAI = GetComponent<EnemyAI>();
    }
    // Update is called once per frame
    void Update()
    {
       EnemyDead();
    }

    // Método que le hace daño al enemigo por un valor puesto en los paréntesis
    public void Damage(float damage)
    {
        // Suena el audio
        //AudioManager.instance.PlayEnemyDamage();

        // Se reduce la salud en el daño
        health -= damage;

        // Se crea una instancia del efecto del daño
       GameObject gunShotEffect = Instantiate(gunHit, transform.position, transform.rotation);

        // El efecto se destrute luego de .5 segundos
       Destroy(gunShotEffect,0.5f);
    }

    // Elimina el enemigo
    public void EnemyDead()
    {
        if (health <= 0)
        {
            // Si la vida del enemigo llega a 0, se remueve el enemgio de la lista de enemgigos disponibles a disparar
            // Y se destruye
            EnemyManager.instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    // Cambio de material cuando enemigo entra en rango o no
    public void MaterialChange()
    {
        if (_meshRenderer.sharedMaterial == originalMaterial)
        {
            _meshRenderer.sharedMaterial = detectedMaterial;
        }
        else
        {
            _meshRenderer.sharedMaterial = originalMaterial;
        }
    }

    public void OnDamageZoneTriggered(Collider other)
    {
        // Comprueba que sea el collider correcto (por nombre o tag)
        if (other != null && other.name == "AreaDaño")
        {
            if (enemyAI != null && enemyAI.IsChasing())
            {
                enemyAI.PauseChase(1f);
            }
        }
    }

}
