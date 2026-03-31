using UnityEngine;

public class Canon : MonoBehaviour
{
    private float timer;
    public float timeToShoot = 5f;


    public float PushForce = 4f;
    public GameObject Ball;
    void Start()
    {
        
    }

    void Update()
    {
        if (timer <=0)
        {
           
            GameObject BallInstance = Instantiate(Ball, transform.position, Quaternion.identity, this.transform);
            
            Vector3 PushDir = (BallInstance.transform.position - transform.position).normalized;


            //Ball.AddForce(PushDir * PushForce, ForceMode.Impulse);



            timer = timeToShoot;
        }

        timer -= Time.deltaTime;
    }

}
