using Assets.Common;
using Assets.Scripts;
using UnityEngine;

public class BarrierPU : MonoBehaviour
{
    [SerializeField]
    bool enable = true;

    SpriteRenderer sprite;
    ParticleSystem explosion;

    GameObject powerUpSpawnArea;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        explosion = GetComponent<ParticleSystem>();
        powerUpSpawnArea = GameObject.Find("PUSpawnArea");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Ball") && enable)
        {
            enable = false;

            sprite.enabled = false;
             
            if (!explosion.isPlaying)
                explosion.Play();

            var ballScript = collision.gameObject.GetComponent<Ball>();

            if (ballScript.lastTouch == PlayerType.player1)
            {
                var barrierPU = GameObject.Find("BarrierPULeft");

                var spawn1 = barrierPU.gameObject.transform.Find("Spawn1").GetComponent<SpawnBarrier>();

                var spawn2 = barrierPU.gameObject.transform.Find("Spawn2").GetComponent<SpawnBarrier>();

                if (!spawn1.flagActivated)
                {
                    spawn1.ActivateSpawn();
                    Destroy(gameObject, explosion.main.duration);
                    powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BarrierPU");
                    return;
                }

                else if (spawn1.flagActivated && !spawn2.flagActivated)
                {
                    spawn2.ActivateSpawn();
                    Destroy(gameObject, explosion.main.duration);
                    powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BarrierPU");
                    return;
                }

                else
                {
                    if (Random.Range(0, 1) == 0)
                    {
                        spawn1.ActivateSpawn();
                        Destroy(gameObject, explosion.main.duration);
                        powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BarrierPU");
                        return;
                    }
                    else
                    {
                        spawn2.ActivateSpawn();
                        Destroy(gameObject, explosion.main.duration);
                        powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BarrierPU");
                        return;
                    }
                }
            }

            if (ballScript.lastTouch == PlayerType.player2 || ballScript.lastTouch == PlayerType.CPU)
            {
                var barrierPU = GameObject.Find("BarrierPURight");

                var spawn1 = barrierPU.gameObject.transform.Find("Spawn1").GetComponent<SpawnBarrier>();

                var spawn2 = barrierPU.gameObject.transform.Find("Spawn2").GetComponent<SpawnBarrier>();

                if (!spawn1.flagActivated)
                {
                    spawn1.ActivateSpawn();
                    Destroy(gameObject, explosion.main.duration);
                    powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BarrierPU");
                    return;
                }

                else if (spawn1.flagActivated && !spawn2.flagActivated)
                {
                    spawn2.ActivateSpawn();
                    Destroy(gameObject, explosion.main.duration);
                    powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BarrierPU");
                    return;
                }

                else
                {
                    if (Random.Range(0, 1) == 0)
                    {
                        spawn1.ActivateSpawn();
                        Destroy(gameObject, explosion.main.duration);
                        powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BarrierPU");
                        return;
                    }
                    else
                    {
                        spawn2.ActivateSpawn();
                        Destroy(gameObject, explosion.main.duration);
                        powerUpSpawnArea.GetComponent<PowerUpSpawnArea>().PowerUpEnded("BarrierPU");
                        return;
                    }
                }
            }
        }
    }
}
