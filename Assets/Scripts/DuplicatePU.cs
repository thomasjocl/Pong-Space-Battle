using UnityEngine;

public class DuplicatePU : MonoBehaviour
{
    public GameObject ball;
    ParticleSystem explosion;
    SpriteRenderer sprite;

    [SerializeField]
    bool enable;

    GameObject powerUpSpawnArea;

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        powerUpSpawnArea = GameObject.Find("PUSpawnArea");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var yVel = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;

        var xVel = Mathf.Atan(Random.Range(45, 135) * Mathf.Deg2Rad) * yVel;

        if (enable)
        {
            var _vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

            var _ball = Instantiate(ball);

            _ball.transform.position = collision.transform.position;

            var _ballBehaviour = _ball.transform.Find("Ball_Behaviour");

            _ballBehaviour.transform.localScale = new Vector3(0.5f, 0.5f, 0);

            _ballBehaviour.GetComponent<Ball>().state = Ball.State.normal;

            _ballBehaviour.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel * -1);

            _ballBehaviour.GetComponent<Ball>().type = Ball.Type.duplicate_ball;
        }

        collision.gameObject.GetComponent<Ball>().ChangeDuplicateBallState();

        enable = false;

        sprite.enabled = false;

        if(!explosion.isPlaying)
            explosion.Play();

        Destroy(gameObject, explosion.main.duration);

        powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("DuplicatePU");
    }
}
