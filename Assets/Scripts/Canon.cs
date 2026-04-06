using UnityEngine;

public class Canon : MonoBehaviour
{
    private float timer;
    public float timeToShoot = 5f;


    public float PushForce;
    public GameObject Ball;
    void Start()
    {
        
    }

    void Update()
    {
        if (timer <=0)
        {
           PushForce = Random.Range(7f, 17f);
            GameObject BallInstance = Instantiate(Ball, transform.position, Quaternion.identity);
            
            //Vector3 PushDir = (BallInstance.transform.position - transform.position).normalized;

            BallInstance.transform.forward = transform.forward;

            BallInstance.GetComponent<Rigidbody>().AddForce(transform.forward * PushForce, ForceMode.Impulse);


            timer = timeToShoot;
        }

        timer -= Time.deltaTime;
    }

}
