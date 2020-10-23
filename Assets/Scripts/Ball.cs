using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Camera cam;
    Vector2 mousePos;
    Vector2 lookDir;
    SpriteRenderer sprite;
    Light2D light;

    public enum State { speed_ball, duplicate_ball, simple_ball }

    public State state;

    public bool flagOriginal;

    public bool maxVelEnable;
    public Vector2 maxVelocity;

    static readonly BallProperties duplicateBall = new BallProperties { ColorSprite = new Color(255, 100, 100), ColorGlow = new Color(250, 36, 36) };
    static readonly BallProperties speedBall = new BallProperties { ColorSprite = new Color(122, 255, 114), ColorGlow = new Color(190, 255, 114) };

    float simpleSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        light = GetComponent<Light2D>();
    }

    void Update()
    {
        if (maxVelEnable)
        {
            if (Mathf.Abs(rb.velocity.x) != maxVelocity.x && Mathf.Abs(rb.velocity.y) != maxVelocity.y)
                rb.velocity = new Vector2(maxVelocity.x, maxVelocity.y);
        }
        else
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
                foreach (var ball in balls)
                {
                    if (!ball.GetComponent<Ball>().flagOriginal)
                        Destroy(ball);
                }
            }

            if(Input.GetKeyDown(KeyCode.Z))
                GetComponent<SpriteRenderer>().color = duplicateBall.ColorSprite;
        }
    }

    public void ChangeDuplicateBallState()
    {
        state = State.duplicate_ball;
        sprite.color = duplicateBall.ColorSprite;
        //light.color = duplicateBall.ColorGlow;
    }

    public void ChangeSpeedBallState(float boost)
    {
        state = State.speed_ball;
        sprite.color = speedBall.ColorSprite;
        light.color = speedBall.ColorGlow; 
        simpleSpeed = speed;
        speed *= boost;
    }

    public void ChangeSimpleBallState()
    {
        state = State.simple_ball;
        speed = simpleSpeed;
    }
}

public class BallProperties
{
    public Color ColorSprite { get; set; }
    public Color ColorGlow { get; set; }
}