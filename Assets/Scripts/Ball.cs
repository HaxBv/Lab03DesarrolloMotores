using UnityEngine;

public class Ball : MonoBehaviour
{
    public float lifeTime = 10f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
