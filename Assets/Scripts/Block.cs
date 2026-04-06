using System.Threading;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float TimeToChangeDirection = 5f;
    public float PauseDuration = 2f;
    public float speed = 1f;

    private float moveTimer;
   
    private float pauseTimer;

    public float Timer;
    public bool MovingRight = false;
    private bool isPaused = false;
    void Start()
    {
        moveTimer = TimeToChangeDirection;
    }

    void Update()
    {
        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;

            if (pauseTimer <= 0)
            {
                isPaused = false;

                MovingRight = !MovingRight;

                moveTimer = TimeToChangeDirection;
            }

            return;
        }

        moveTimer -= Time.deltaTime;

        if (MovingRight)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }

        if (moveTimer <= 0)
        {
            isPaused = true;
            pauseTimer = PauseDuration;
        }
    }
}
