using System;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnArea : MonoBehaviour
{
    [SerializeField]
    List<PowerUp> powerUps;

    [SerializeField]
    float minX, maxX, minY, maxY;

    [SerializeField]
    float respawnTime, initRespawnTime;

    [SerializeField]
    int maxPowersOnScreen;
     
    float nextTimeToSpawnPower;

    // Start is called before the first frame update
    void Start()
    {
        nextTimeToSpawnPower = Time.time + initRespawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        int countPowersOnScreen = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        
        if(countPowersOnScreen <= maxPowersOnScreen && Time.time > nextTimeToSpawnPower)
        {
        }

        Debug.Log(countPowersOnScreen);
    }
}

[Serializable]
public class PowerUp
{
    public GameObject Prefab;

    [Range(0f,100f)]
    public float Probability;
}
