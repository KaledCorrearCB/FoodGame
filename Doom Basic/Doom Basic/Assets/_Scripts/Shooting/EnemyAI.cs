using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Referencias")]
    private NavMeshAgent agent;
    private Transform player;

    [Header("Patrullaje")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    [Header("Detecci�n")]
    public float visionRadius = 10f;
    public float loseSightTime = 3f;
    private float loseSightTimer;
    private bool playerInSight;
    private Vector3 lastKnownPosition;

    [Header("Velocidades")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    private enum EnemyState { Patrolling, Chasing, Searching }
    private EnemyState currentState = EnemyState.Patrolling;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GoToNextPatrolPoint();
        agent.autoBraking = false;
        agent.autoRepath = false;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // --- DETECCI�N ---
        if (distanceToPlayer <= visionRadius)
        {
            playerInSight = true;
            lastKnownPosition = player.position;
            loseSightTimer = 0;
        }
        else if (playerInSight)
        {
            loseSightTimer += Time.deltaTime;
            if (loseSightTimer > loseSightTime)
                playerInSight = false;
        }

        // --- M�QUINA DE ESTADOS ---
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                if (playerInSight) ChangeState(EnemyState.Chasing);
                break;

            case EnemyState.Chasing:
                Chase();
                if (!playerInSight) ChangeState(EnemyState.Searching);
                break;

            case EnemyState.Searching:
                Search();
                break;
        }
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case EnemyState.Patrolling:
                agent.speed = patrolSpeed;
                GoToNextPatrolPoint();
                break;

            case EnemyState.Chasing:
                agent.speed = chaseSpeed;
                break;

            case EnemyState.Searching:
                agent.speed = patrolSpeed;
                agent.SetDestination(lastKnownPosition);
                break;
        }
    }

    private void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GoToNextPatrolPoint();
    }

    private void Chase()
    {
        if (player != null)
            agent.SetDestination(player.position);
    }

    private void Search()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Si llega al �ltimo punto conocido y no encuentra al jugador, vuelve a patrullar
            ChangeState(EnemyState.Patrolling);
        }
    }

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}