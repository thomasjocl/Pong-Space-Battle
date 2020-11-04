using Assets.Scripts;
using UnityEngine;

public class BarrierPU : MonoBehaviour
{
    [SerializeField]
    GameObject barrierLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        { 
            barrierLeft.transform.Find("Spawn1").GetComponent<SpawnBarrier>().ActivateSpawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
        {

        }
    } 
} 
