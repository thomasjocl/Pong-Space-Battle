using Assets.Common;
using Assets.Scripts;
using UnityEngine;

public class GolArea : MonoBehaviour
{
    [SerializeField]
    PlayerType playerType;

    [SerializeField]
    bool enable;

    GameObject score;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.CompareTag("Ball"))
        {
            score.GetComponent<Score>().AddPoint(playerType);

            if(enable)
                Destroy(collision.gameObject.transform.parent);
        }
    }
}
