using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedPU : MonoBehaviour
{
    ParticleSystem explosion;
    SpriteRenderer sprite;
    bool enable;
    public float boost;

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
        if (enable)
        {
            collision.gameObject.GetComponent<Ball>().ChangeSpeedBallState(boost);
        }

        enable = false;

        sprite.enabled = false;

        explosion.Play();

        Destroy(gameObject, explosion.main.duration);
    }
}
