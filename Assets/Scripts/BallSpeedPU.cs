using UnityEngine;

public class BallSpeedPU : MonoBehaviour
{
    ParticleSystem explosion;
    SpriteRenderer sprite;
    
    [SerializeField]
    bool enable;
    
    public float boost;

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
        if (enable)
        {
            collision.gameObject.GetComponent<Ball>().ChangeSpeedBallState(boost);
        }

        enable = false;

        sprite.enabled = false;
         
        if (!explosion.isPlaying)
            explosion.Play();

        Destroy(gameObject, explosion.main.duration);

        powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BallSpeedPU");
    }
}
