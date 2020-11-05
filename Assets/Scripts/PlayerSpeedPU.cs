using Assets.Common;
using UnityEngine;

public class PlayerSpeedPU : MonoBehaviour
{
    ParticleSystem explosion;
    SpriteRenderer sprite;

    [SerializeField]
    bool enable; 

    [SerializeField]
    float speedBoostTime;

    [SerializeField]
    float speedBoostMultiplier;

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
        if (collision.gameObject.transform.CompareTag("Ball") && enable)
        {
            var ballScript = collision.gameObject.GetComponent<Ball>();

            if(ballScript.lastTouch == PlayerType.player1)
            {
                var p1Script = GameObject.Find("Player1").GetComponent<Player>();
                p1Script.BoostSpeed(speedBoostTime, speedBoostMultiplier);
            }

            if (ballScript.lastTouch == PlayerType.player2 || ballScript.lastTouch == PlayerType.IA)
            {
                var p2Script = GameObject.Find("Player2").GetComponent<Player>();
                p2Script.BoostSpeed(speedBoostTime, speedBoostMultiplier);
            }

            enable = false;

            sprite.enabled = false;

            explosion.Play();

            Destroy(gameObject, explosion.main.duration);
        }
    }
}
