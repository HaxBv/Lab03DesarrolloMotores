using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    public float currentTimer;
    public float TimeToSpawn = 2f;

    public float Range = 10f;
    public int AmountEnemiesToSpawn;

    public int MaxEnemiesOnScreen;
    public int currentEnemiesOnScreen;


    public Transform PlayerReference;
    public GameObject EnemyReference;

    void Start()
    {
        GetComponent<SphereCollider>().radius = Range;

    }

    void Update()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer > TimeToSpawn)
        {
            if (currentEnemiesOnScreen < MaxEnemiesOnScreen)
            {
                for (int i = 0; i < AmountEnemiesToSpawn; i++)
                {

                    SpawnNormalEnemy();
                    
                    currentTimer = 0f;

                    Debug.Log("EnemyCreate");
                }
            }

        }

    }


    public void SpawnNormalEnemy()
    {


        currentEnemiesOnScreen++;
        GameObject obj = Instantiate(EnemyReference);



        Vector3 origin = transform.position;
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;


        Vector3 finalPositon = origin + dir * Random.Range(0, Range);


        obj.transform.position = finalPositon;




        AgentEnemyController enemy = obj.GetComponent<AgentEnemyController>();


        enemy.spawner = this;
        enemy.Player = PlayerReference;
    }

 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
