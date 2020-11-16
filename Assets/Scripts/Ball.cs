using Assets.Common;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Camera cam;
    Vector2 mousePos;
    Vector2 lookDir;
    SpriteRenderer sprite;
    Light2D light;
    ParticleSystem particle;

    public enum State { speed_ball, duplicate_ball, simple_ball }

    public State state;

    public Vector2 maxVelocity;

    static readonly BallProperties duplicateBall = new BallProperties { ColorSprite = new Color(1f, 0.39f, 0.39f), ColorGlow = new Color(1f, 0.45f, 0.45f) };
    static readonly BallProperties speedBall = new BallProperties { ColorSprite = new Color(0.6f, 1f, 0.6f), ColorGlow = new Color(0.75f, 1f, 0.45f) };

    public bool flagIncrementVelocity;

    public PlayerType lastTouch;

    float simpleSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        light = GetComponent<Light2D>();
        particle = GetComponent<ParticleSystem>();

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (state == State.duplicate_ball)
            ChangeDuplicateBallState();
    }

    void Update()
    {
        var v = rb.velocity.normalized;
        v *= speed;
        rb.velocity = v;

        if (Mathf.Abs(rb.velocity.x) > maxVelocity.x)
            rb.velocity = new Vector2(maxVelocity.x, rb.velocity.y) * new Vector2((rb.velocity.x < 0) ? -1 : 1, 1);

        if (Mathf.Abs(rb.velocity.y) > maxVelocity.y)
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity.y) * new Vector2(1, (rb.velocity.y < 0) ? -1 : 1);

        if ((Mathf.Abs(rb.velocity.x) > maxVelocity.x) || (Mathf.Abs(rb.velocity.y) > maxVelocity.y))
            flagIncrementVelocity = false;


        if (Input.GetKeyDown(KeyCode.Mouse0) && cam != null)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            lookDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
            rb.velocity = new Vector2(lookDir.x, lookDir.y).normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var v = rb.velocity.normalized;

        if (collision.transform.CompareTag("Ball"))
            particle.Emit(100);

        if (collision.transform.CompareTag("BarrierDefault"))
        {
             if(v.x < 0.25) 
                rb.velocity = new Vector2(rb.velocity.x * 2, rb.velocity.y); 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("BarrierDefault"))
        { 
            if (rb.velocity.y == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, (Random.Range(0.5f, 2.5f) * ((transform.position.y > 0) ? 1 : -1)));
            }
        }
    }

    public void ChangeDuplicateBallState()
    {
        state = State.duplicate_ball;
        sprite.color = duplicateBall.ColorSprite;
        light.color = duplicateBall.ColorGlow;
        particle.startColor = duplicateBall.ColorSprite;
    }

    public void ChangeSpeedBallState(float boost)
    {
        Debug.Log("old speed " + speed);
        state = State.speed_ball;
        sprite.color = speedBall.ColorSprite;
        light.color = speedBall.ColorGlow;
        //simpleSpeed = speed;
        speed *= boost;
        Debug.Log("new speed " + speed);
        particle.startColor = duplicateBall.ColorSprite;
    }

    public void ChangeSimpleBallState()
    {
        state = State.simple_ball;
        //speed = simpleSpeed;
    }

    public void ChangeTouchedByState(PlayerType touchedBy)
    {
        lastTouch = touchedBy;
    }
}

public class BallProperties
{
    public Color ColorSprite { get; set; }
    public Color ColorGlow { get; set; }
}