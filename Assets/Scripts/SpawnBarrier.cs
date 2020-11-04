using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnBarrier : MonoBehaviour
    {
        bool flagActivated;

        float endTime;

        [SerializeField]
        float duration;

        [SerializeField]
        Sprite barrierSprite;

        GameObject barrier1;
        GameObject barrier2;

        [SerializeField]
        float maxHeight;

        [SerializeField]
        float scaleToModify;

        private void Start()
        {

            if (gameObject.transform.Find("Barrier1") != null)
                barrier1 = gameObject.transform.Find("Barrier1").gameObject;

            if (gameObject.transform.Find("Barrier2") != null)
                barrier2 = gameObject.transform.Find("Barrier2").gameObject;
        }

        private void Update()
        {
            if (flagActivated)
            {
                if (barrier1.transform.localScale.y < 4 && barrier2.transform.localScale.y < 4)
                {
                    //var scaleToIncreaseB1 = new Vector3(0, barrier1.transform.localScale.y * increaseScaleVel * Time.fixedDeltaTime, 0);
                    var scaleToIncreaseB1 = new Vector3(0, scaleToModify, 0);

                    if (scaleToIncreaseB1.y + barrier1.transform.localScale.y > 4)
                        scaleToIncreaseB1 = new Vector3(0, (4 - barrier1.transform.localScale.y), 0);

                    barrier1.transform.localScale += scaleToIncreaseB1;

                    //var scaleToIncreaseB2 = new Vector3(0, barrier2.transform.localScale.y * increaseScaleVel * Time.fixedDeltaTime, 0);
                    var scaleToIncreaseB2 = new Vector3(0, scaleToModify, 0);

                    if (scaleToIncreaseB2.y + barrier2.transform.localScale.y > 4)
                        scaleToIncreaseB2 = new Vector3(0, (4 - barrier2.transform.localScale.y), 0);

                    barrier2.transform.localScale += scaleToIncreaseB2; 
                }

                if (Time.time > endTime)
                {
                    flagActivated = false;
                }
            }

            if (!flagActivated && barrier1.transform.localScale.y > 1 && barrier2.transform.localScale.y > 1)
            {
                //var scaleToDecreaseB1 = new Vector3(0, (5 - barrier1.transform.localScale.y) * decreaseScaleVel * Time.deltaTime, 0);
                var scaleToDecreaseB1 = new Vector3(0, scaleToModify, 0);

                if (barrier1.transform.localScale.y - scaleToDecreaseB1.y < 1)
                    scaleToDecreaseB1 = new Vector3(0, (barrier1.transform.localScale.y - 1), 0);

                barrier1.transform.localScale -= scaleToDecreaseB1;

                //var scaleToDecreaseB2 = new Vector3(0, (5 - barrier2.transform.localScale.y) * decreaseScaleVel * Time.deltaTime, 0);
                var scaleToDecreaseB2 = new Vector3(0, scaleToModify, 0);

                if (barrier2.transform.localScale.y - scaleToDecreaseB2.y < 1)
                    scaleToDecreaseB2 = new Vector3(0, (barrier2.transform.localScale.y - 1), 0);

                barrier2.transform.localScale -= scaleToDecreaseB2;
            }
        }

        public void ActivateSpawn()
        {
            flagActivated = true;
            endTime = Time.time + duration;
        }
    }
}
