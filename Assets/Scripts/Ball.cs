using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Camera cam;
    Vector2 mousePos;
    Vector2 lookDir;

    public bool flagOriginal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {   
        var v = rb.velocity.normalized;
        v *= speed;
        rb.velocity = v;

        if (Input.GetKeyDown(KeyCode.Mouse0) && cam != null)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            lookDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            rb.velocity = new Vector2(lookDir.x, lookDir.y).normalized * speed;
            Debug.Log(rb.velocity);
            Debug.Log(rb.velocity.magnitude);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            var balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach(var ball in balls)
            {
                if (!ball.GetComponent<Ball>().flagOriginal)
                    Destroy(ball);
            }
        }
    } 
}
