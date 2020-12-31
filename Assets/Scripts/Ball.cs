using Assets.Common; 
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Ball : MonoBehaviour
{
    [SerializeField]
    bool controlByUser;

    [SerializeField]
    bool isColliderEnabled;

    Rigidbody2D rb;
    public float speed;
    Camera cam;
    Vector2 mousePos;
    Vector2 lookDir;
    SpriteRenderer sprite;
    Light2D light;
    ParticleSystem particleBallBounce;
    ParticleSystem particleBallIntro;

    public enum Type { speed_ball, duplicate_ball, simple_ball }
    public enum State { initializing, normal, disappearing }

    public Type type;

    public State state;

    public Vector2 maxVelocity;

    static readonly BallProperties duplicateBall = new BallProperties { ColorSprite = new Color(1f, 0.39f, 0.39f), ColorGlow = new Color(1f, 0.45f, 0.45f) };
    static readonly BallProperties speedBall = new BallProperties { ColorSprite = new Color(0.6f, 1f, 0.6f), ColorGlow = new Color(0.75f, 1f, 0.45f) };

    public bool flagIncrementVelocity;

    public PlayerType lastTouch;

    bool startIntroSFX = true;

    float endTimeInitializing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        light = GetComponent<Light2D>();

        rb.transform.localScale = new Vector3(0f, 0f, 0f);

        particleBallBounce = transform.parent.Find("SFX_Ball_Bounce").GetComponent<ParticleSystem>();
        particleBallIntro = transform.parent.Find("SFX_Ball_Intro").GetComponent<ParticleSystem>();

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (type == Type.duplicate_ball)
            ChangeDuplicateBallState();
    }

    void Update()
    {
        if (state == State.initializing)
        {
            if (startIntroSFX)
            {
                endTimeInitializing = Time.time + particleBallIntro.main.duration;
                particleBallIntro.Play();
            }

            PlayIntroSFX();
        }
        else
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


            if (Input.GetKeyDown(KeyCode.Mouse0) && cam != null && controlByUser)
            {
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                lookDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
                rb.velocity = new Vector2(lookDir.x, lookDir.y).normalized * speed;
            } 
        }
    }

    public void PlayIntroSFX()
    {
        startIntroSFX = false;

        rb.isKinematic = true;

        if (rb.transform.localScale.y < 0.5)
        {
            rb.transform.localScale = rb.transform.localScale + (new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime);
        }

        if (rb.transform.localScale.y >= 0.5)
        {
            rb.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            if (Time.time > endTimeInitializing && particleBallIntro.particleCount == 0)
            {
                rb.isKinematic = false;
                state = State.normal;

                rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * speed;

                Debug.Log(rb.velocity);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var v = rb.velocity.normalized;

        if (collision.transform.CompareTag("Ball"))
            particleBallBounce.Emit(100);

        if (collision.transform.CompareTag("BarrierDefault"))
        {
            if (v.x < 0.25)
                rb.velocity = new Vector2(rb.velocity.x * 2, rb.velocity.y);
        }

        Debug.Log(rb.velocity);
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
        type = Type.duplicate_ball;
        sprite.color = duplicateBall.ColorSprite;
        light.color = duplicateBall.ColorGlow;
        particleBallBounce.startColor = duplicateBall.ColorSprite;
    }

    public void ChangeSpeedBallState(float boost)
    {
        Debug.Log("old speed " + speed);
        type = Type.speed_ball;
        sprite.color = speedBall.ColorSprite;
        light.color = speedBall.ColorGlow;
        //simpleSpeed = speed;
        speed *= boost;
        Debug.Log("new speed " + speed);
        particleBallBounce.startColor = duplicateBall.ColorSprite;
    }

    public void ChangeSimpleBallState()
    {
        type = Type.simple_ball;
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