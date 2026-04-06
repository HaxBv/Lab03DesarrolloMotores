using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AgentEnemyController : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform Player;

    public EnemySpawner spawner;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();


    }
    void Start()
    {
        agent.speed = Random.Range(2, 5);
        agent.acceleration = Random.Range(5, 10);
        agent.angularSpeed = Random.Range(120, 360);
        agent.stoppingDistance = Random.Range(1, 3);
        agent.avoidancePriority = Random.Range(50, 100);
    }

    void Update()
    {
        if (Player != null)
        {
            agent.SetDestination(Player.position);
            //agent.

        }
    }
    public void Set(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (agent == null || agent.path == null) return;

        Vector3[] corners = agent.path.corners;


        for (int i = 0; i < corners.Length - 1; i++)
        {
            Gizmos.DrawLine(corners[i], corners[i + 1]);
            Gizmos.DrawSphere(corners[i], 0.2f);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.spawner.currentEnemiesOnScreen--;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.rigidbody != null)
        {
            Destroy(gameObject);

        }





    }
}
