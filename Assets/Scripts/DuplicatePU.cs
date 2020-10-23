using UnityEngine;

public class DuplicatePU : MonoBehaviour
{
    public GameObject ball;
    ParticleSystem explosion;
    SpriteRenderer sprite;
    bool enable;

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            enable = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);

        var yVel = collision.gameObject.GetComponent<Rigidbody2D>().velocity.y;
         
        var xVel = Mathf.Atan(Random.Range(45, 135) * Mathf.Deg2Rad) * yVel;

        if (enable)
        {
            var _vel = collision.gameObject.GetComponent<Rigidbody2D>().velocity;

            var _ball = Instantiate(ball);

            _ball.transform.position = collision.transform.position;

            _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel * -1);

            _ball.GetComponent<Ball>().ChangeDuplicateBallState();
        }

        collision.gameObject.GetComponent<Ball>().ChangeDuplicateBallState();

        enable = false;

        sprite.enabled = false;

        explosion.Play(); 

        Destroy(gameObject, explosion.main.duration);
    }
}
