using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnBarrier : MonoBehaviour
    {
        [SerializeField]
        bool flagActivated;

        [SerializeField]
        TimeSpan startTime;

        [SerializeField]
        Sprite barrierSprite;

        GameObject barrier1;
        GameObject barrier2;

        [SerializeField]
        float maxHeight;

        [SerializeField]
        float scaleVelocity;

        private void Start()
        {
            if(gameObject.transform.Find("Barrier1") != null) 
                barrier1 = gameObject.transform.Find("Barrier1").gameObject;
            
            if (gameObject.transform.Find("Barrier2") != null)
                barrier2 = gameObject.transform.Find("Barrier2").gameObject;
        }

        private void Update()
        {
            if (flagActivated)
            {
                var scaleToAdd = new Vector3(0, barrier1.transform.localScale.y * scaleVelocity * Time.deltaTime, 0);
                barrier1.transform.localScale += scaleToAdd;
            }
        }

        public void ActivateSpawn()
        { 
            flagActivated = true; 
        }
    }
}
