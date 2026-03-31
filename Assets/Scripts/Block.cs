using System.Threading;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float TimeToChangeDirection = 5f;

    public float speed = 1f;


    public float Timer;
    public bool MovingRight = false;
    void Start()
    {
        
    }

    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Timer = TimeToChangeDirection;
            if(MovingRight)
            {

                MovingRight = false;
            }
            else
            {
                MovingRight = true;
            }
        }
        if (MovingRight)
        {

            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }



    }
}
