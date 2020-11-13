using Assets.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolArea : MonoBehaviour
{
    [SerializeField]
    PlayerType playerType;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
        }
    }
}
