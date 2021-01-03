using Assets.Common;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class Score : MonoBehaviour
    {
        [SerializeField]
        public int P1Score, P2Score;

        TextMeshProUGUI p1ScoreTMP;
        TextMeshProUGUI p2ScoreTMP;

        void Start()
        {
            p1ScoreTMP = gameObject.transform.Find("Canvas").Find("P1ScoreTMP").GetComponent<TextMeshProUGUI>();
            p2ScoreTMP = gameObject.transform.Find("Canvas").Find("P2ScoreTMP").GetComponent<TextMeshProUGUI>();
        }

        public void AddPoint(PlayerType playerType)
        {
            if (playerType == PlayerType.player1)
            {
                P1Score++;
                p1ScoreTMP.SetText(P1Score.ToString());
                StartCoroutine(ShakeScore(p1ScoreTMP));
            }
            else
            {
                P2Score++;
                p2ScoreTMP.SetText(P2Score.ToString());
                StartCoroutine(ShakeScore(p2ScoreTMP));
            }
        }

        IEnumerator ShakeScore(TextMeshProUGUI score)
        {
            for (float i = 1f; i <= 1.2f; i += 0.05f)
            {
                score.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }

            score.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

            for (float i = 1.2f; i >= 1f; i -= 0.05f)
            {
                score.rectTransform.localScale = new Vector3(i, i, i);
                yield return new WaitForEndOfFrame();
            }

            score.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
