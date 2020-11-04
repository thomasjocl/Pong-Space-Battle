using Assets.Common;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    Rigidbody2D rb;

    [SerializeField]
    Vector2 movePosition;

    [SerializeField]
    float speedMovement;

    [SerializeField]
    float speedBoost;

    [SerializeField]
    PlayerType playerType;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        movePosition.x = 0;

        if (playerType == PlayerType.player1)
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
                movePosition.y = 0;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                movePosition.y = Input.GetAxisRaw("P1Movement");
        }

        if (playerType == PlayerType.player2)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                movePosition.y = 0;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                movePosition.y = Input.GetAxisRaw("P2Movement");
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movePosition * speedMovement * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            var ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
            
            var ballScript = collision.gameObject.GetComponent<Ball>();

            ballScript.ChangeTouchedByState(playerType);

            ballRB.velocity = new Vector2(ballRB.velocity.x * speedBoost, ((1 + ((movePosition.y != 0) ? Random.Range(0.2f, 0.6f) : 0)) * ballRB.velocity.y));

            if (ballRB.velocity.y == 0)
                ballRB.velocity = new Vector2(ballRB.velocity.x, Random.Range(2.5f, 7.5f));

            if(Mathf.Abs(ballRB.velocity.normalized.y) > 0.85f || Mathf.Abs(ballRB.velocity.normalized.y) < 0.15f)
            {
                if (ballRB.velocity.y > 0)
                    ballRB.velocity = new Vector2(ballRB.velocity.x, Random.Range(0.5f, 0.8f));
                else if (ballRB.velocity.y < 0)
                    ballRB.velocity = new Vector2(ballRB.velocity.x, Random.Range(-0.5f, -0.8f));
            }

            collision.gameObject.GetComponent<Ball>().speed += 0.5f;

            Debug.Log("vel: " + ballRB.velocity + ", norm: " + ballRB.velocity.normalized);
        }
    }
}
