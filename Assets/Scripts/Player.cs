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
    PlayerType playerType;

    float speedBoostPlayer = 1f;

    float speedBoostEndTime;

    bool flagSpeedBoostActive;

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

        if (flagSpeedBoostActive)
        {
            if (Time.time > speedBoostEndTime)
            {
                flagSpeedBoostActive = false;
                speedBoostPlayer = 1f;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movePosition * speedMovement * speedBoostPlayer * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.parent != null && collision.transform.parent.CompareTag("Ball"))
        {
            var ballRB = collision.gameObject.GetComponent<Rigidbody2D>();

            var ballScript = collision.gameObject.GetComponent<Ball>();

            ballScript.ChangeTouchedByState(playerType);

            var yAxisSpeedModifier = UnityEngine.Random.Range(1.5f, 2.5f);

            ballRB.velocity = new Vector2(ballRB.velocity.x, ballRB.velocity.y + (movePosition.y * yAxisSpeedModifier));

            //ballRB.velocity = new Vector2(ballRB.velocity.x * speedBoostBall, ((1 + ((movePosition.y != 0) ? Random.Range(0.2f, 0.6f) : 0)) * ballRB.velocity.y));

            if (ballRB.velocity.y == 0)
                ballRB.velocity = new Vector2(ballRB.velocity.x, Random.Range(2.5f, 7.5f));

            if (Mathf.Abs(ballRB.velocity.normalized.y) > 0.85f || Mathf.Abs(ballRB.velocity.normalized.y) < 0.15f)
            {
                if (ballRB.velocity.y > 0)
                    ballRB.velocity = new Vector2(ballRB.velocity.normalized.x, Random.Range(0.5f, 0.8f));
                else if (ballRB.velocity.y < 0)
                    ballRB.velocity = new Vector2(ballRB.velocity.normalized.x, Random.Range(-0.5f, -0.8f));
            }

            if (Mathf.Abs(ballRB.velocity.normalized.x) < 0.15f)
            {
                if (playerType == PlayerType.player1)
                    ballRB.velocity = new Vector2(Random.Range(0.5f, 0.8f), ballRB.velocity.normalized.y);
                else
                    ballRB.velocity = new Vector2(Random.Range(-0.8f, -0.5f), ballRB.velocity.normalized.y);
            }

            ballScript.speed += 0.5f;
        }
    }

    public void BoostSpeed(float speedBoostTime, float speedBoostMultipler)
    {
        speedBoostEndTime = Time.time + speedBoostTime;
        speedBoostPlayer = speedBoostMultipler;
        flagSpeedBoostActive = true;
    }
}
