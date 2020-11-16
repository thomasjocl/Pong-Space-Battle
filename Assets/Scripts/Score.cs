using Assets.Common;
using UnityEngine;

namespace Assets.Scripts
{
    public class Score: MonoBehaviour
    {
        [SerializeField]
        public int P1Score;

        [SerializeField]
        public int P2Score;

        public void AddPoint(PlayerType playerType)
        {
            if (playerType == PlayerType.player1)
                P1Score++;
            else
                P2Score++;

            Debug.Log("P1: " + P1Score.ToString() + ", P2: " + P2Score.ToString());
        }
    }
}
