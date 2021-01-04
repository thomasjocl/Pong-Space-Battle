using Assets.Common;
using System.Linq;
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

    Vector2 objBallDirectionToMoveCPU;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(playerType == PlayerType.none)
        {
            if (SceneParameters.GameType == GameType.vsCpu)
                playerType = PlayerType.CPU;
            if (SceneParameters.GameType == GameType.vsP2)
                playerType = PlayerType.player2;
        }
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


        if (playerType == PlayerType.CPU)
        {
            var balls = GameObject.FindGameObjectsWithTag("Ball");

            float distancetoBall = 0f;

            foreach (var ball in balls.Where(x => x.transform.Find("Ball_Behaviour").GetComponent<Rigidbody2D>().velocity.x > 0))
            {
                var distance = Vector2.Distance(ball.transform.Find("Ball_Behaviour").transform.position, transform.position);

                if (distancetoBall == 0)
                {
                    distancetoBall = distance;
                }

                if (distance <= distancetoBall)
                {
                    distancetoBall = distance;
                    objBallDirectionToMoveCPU = ball.transform.Find("Ball_Behaviour").transform.position;
                }
            }

            if (balls.Where(x => x.transform.Find("Ball_Behaviour").GetComponent<Rigidbody2D>().velocity.x > 0).Count() != 0)
            {
                var movY = objBallDirectionToMoveCPU.y - transform.position.y;

                if(Mathf.Abs(movY) > 0.5f)
                {
                    movePosition.y = (new Vector2(0, objBallDirectionToMoveCPU.y - transform.position.y)).normalized.y;
                }
                else
                {
                    movePosition.y = movY;
                }

                //movePosition.y = (new Vector2(0, objBallDirectionToMoveCPU.y - transform.position.y)).normalized.y;
                //movePosition.y = movY;
                Debug.Log(movY);
                Debug.Log((new Vector2(0, objBallDirectionToMoveCPU.y - transform.position.y)).normalized.y);
            }
            else
            {
                movePosition.y = 0;
            }
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
        //if(playerType == PlayerType.player1 || playerType == PlayerType.player2)
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
