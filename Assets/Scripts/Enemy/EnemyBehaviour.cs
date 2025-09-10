using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    [Header("Detecting Player")]
    [SerializeField] private float detectionRadius = 10f;

    [Header("References")]
    [SerializeField] private Transform player;

    private NavMeshAgent enemyAgent;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        PlayerID playerID = FindFirstObjectByType<PlayerID>();

        if (playerID != null && playerID.isPlayer) { player = playerID.transform; }
    }


    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            enemyAgent.SetDestination(player.position);
        }
        else
        {
            enemyAgent.ResetPath();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}