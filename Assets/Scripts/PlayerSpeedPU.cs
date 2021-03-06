﻿using Assets.Common;
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
        if (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.CompareTag("Ball") && enable)
        {
            var ballScript = collision.gameObject.GetComponent<Ball>();

            if(ballScript.lastTouch == PlayerType.player1)
            {
                var p1Script = GameObject.Find("Player1").GetComponent<Player>();
                p1Script.BoostSpeed(speedBoostTime, speedBoostMultiplier);
            }

            if (ballScript.lastTouch == PlayerType.player2 || ballScript.lastTouch == PlayerType.CPU)
            {
                var p2Script = GameObject.Find("Player2").GetComponent<Player>();
                p2Script.BoostSpeed(speedBoostTime, speedBoostMultiplier);
            }

            enable = false;

            sprite.enabled = false;
             
            if (!explosion.isPlaying)
                explosion.Play();

            Destroy(gameObject, explosion.main.duration);

            powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("PlayerSpeedPU");
        }
    }
}
