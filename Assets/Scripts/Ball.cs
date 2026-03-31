using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody body;
    void Start()
    {
       body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
