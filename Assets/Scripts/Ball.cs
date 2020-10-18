using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
     
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(5f, 5f);
    }
     
    void Update()
    {
        
    }
}
