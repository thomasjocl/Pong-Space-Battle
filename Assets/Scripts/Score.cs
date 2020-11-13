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

        void AddPoint(PlayerType playerType)
        {
            if (playerType == PlayerType.player1)
                P1Score++;
            else
                P2Score++;
        }
    }
}
