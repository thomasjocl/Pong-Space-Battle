using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PowerUpSpawnArea : MonoBehaviour
{
    [SerializeField]
    List<PowerUp> powerUps;

    [SerializeField]
    int minX, maxX, minY, maxY;

    [SerializeField]
    float respawnTime, initRespawnTime;

    [SerializeField]
    int maxPowersOnScreen;

    float nextTimeToSpawnPower;

    float enable;

    // Start is called before the first frame update
    void Start()
    {
        nextTimeToSpawnPower = Time.time + initRespawnTime;

        if (powerUps.Any(x => x.Prefab == null))
        {
            Debug.LogError("Uno de los powerup prefab no ha sido asignado");
            EditorApplication.isPlaying = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int countPowersOnScreen = GameObject.FindGameObjectsWithTag("PowerUp").Length;

        if (countPowersOnScreen < maxPowersOnScreen && Time.time > nextTimeToSpawnPower)
        {
            var powerup = SpawnPowerUp();
            
            if (powerup == null)
                return;
            
            nextTimeToSpawnPower = Time.time + respawnTime;

            Instantiate(powerup, new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 0), Quaternion.identity);
        }
    }

    public void PowerUpEnded(string powerup)
    {
        if (powerUps.FirstOrDefault(x => x.Prefab.gameObject.name == powerup).CurrentOnScreen != 0)
        {
            powerUps.FirstOrDefault(x => x.Prefab.gameObject.name == powerup).CurrentOnScreen--;
        }
    }

    GameObject SpawnPowerUp()
    {
        var prob = UnityEngine.Random.Range(0f, 1f);

        powerUps.OrderBy(x => x.Probability).ToList();

        float totalProb = 0f;

        foreach (var powerup in powerUps)
        {
            if (prob <= powerup.RealProbability / 100 && (powerup.CurrentOnScreen < powerup.MaxCurrentOnScreen || powerup.MaxCurrentOnScreen == 0))
            {
                powerup.CurrentOnScreen++;
                return powerup.Prefab;
            }
            totalProb += powerup.RealProbability;
        }

        return null;
    }
}

[Serializable]
public class PowerUp
{
    public GameObject Prefab;

    [Range(0f, 100f)]
    public float Probability;

    public float RealProbability;

    public int MaxCurrentOnScreen;

    public int CurrentOnScreen;
}
