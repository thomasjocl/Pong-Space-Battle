using Assets.Scripts;
using UnityEngine;

public class BallSpawnArea : MonoBehaviour
{
    [SerializeField]
    GameObject ball;

    [SerializeField]
    GameObject scoreGO;

    [SerializeField]
    float timeAddToNextLevel;

    [SerializeField]
    int maxPointsAddToNextLevel;

    [SerializeField]
    int maxPointsToNextLevel;

    int p1Score, p2Score;

    float timeToNextLevel;

    [SerializeField]
    int level = 1;

    void Start()
    {
        timeToNextLevel = Time.time + 20;
    }

    // Update is called once per frame
    void Update()
    {
        p1Score = scoreGO.transform.GetComponent<Score>().P1Score;
        p2Score = scoreGO.transform.GetComponent<Score>().P2Score;

        var totalScore = p1Score + p2Score;

        CheckLevel(totalScore);

        CheckBallsOnScene();
    }

    void CheckLevel(int totalScore)
    {
        if (level == 3)
        {
            return;
        }

        if(Time.time > timeToNextLevel || totalScore >= maxPointsToNextLevel)
        {
            level++;
            maxPointsToNextLevel += maxPointsAddToNextLevel;
            timeToNextLevel = Time.time + timeAddToNextLevel;
            return;
        } 

        return;

    }

    void CheckBallsOnScene()
    {
        var ballsOnScreen = GameObject.FindGameObjectsWithTag("Ball").Length;

        if (level == 1 && ballsOnScreen == 0)
        {
            var ballInst = Instantiate(ball, new Vector3(0f, 0f, 0f), Quaternion.identity);

            ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
            ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;
        }

        if (level == 2 && ballsOnScreen < 2)
        {
            int ballsToSpawn = 2 - ballsOnScreen;
             
            bool validPos1 = isClearZoneToSpawn(0f, 4.5f);

            bool validPos2 = isClearZoneToSpawn(0f, 0f);

            bool validPos3 = isClearZoneToSpawn(0f, -4.5f);

            if (validPos1 && validPos2 && validPos3)
            {
                if (ballsToSpawn == 1)
                {
                    var ballInst = Instantiate(ball, new Vector3(0f, 0f, 0f), Quaternion.identity);

                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;
                }

                if (ballsToSpawn == 2)
                {
                    var ballInst = Instantiate(ball, new Vector3(0f, 4.5f, 0f), Quaternion.identity);

                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;

                    var ballInst1 = Instantiate(ball, new Vector3(0f, -4.5f, 0f), Quaternion.identity);

                    ballInst1.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst1.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;
                }
            }
        }


        if (level == 3 && ballsOnScreen < 3)
        {
            int ballsToSpawn = 3 - ballsOnScreen;

            bool validPos1 = isClearZoneToSpawn(0f, 4.5f);

            bool validPos2 = isClearZoneToSpawn(0f, 0f);

            bool validPos3 = isClearZoneToSpawn(0f, -4.5f);

            if (validPos1 && validPos2 && validPos3)
            {

                if (ballsToSpawn == 1)
                {
                    var ballInst = Instantiate(ball, new Vector3(0f, 0f, 0f), Quaternion.identity);

                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;
                }

                if (ballsToSpawn == 2)
                {
                    var ballInst = Instantiate(ball, new Vector3(0f, 4.5f, 0f), Quaternion.identity);

                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;

                    var ballInst1 = Instantiate(ball, new Vector3(0f, -4.5f, 0f), Quaternion.identity);

                    ballInst1.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst1.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;
                }

                if (ballsToSpawn == 3)
                {
                    var ballInst = Instantiate(ball, new Vector3(0f, 4.5f, 0f), Quaternion.identity);

                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;

                    var ballInst1 = Instantiate(ball, new Vector3(0f, 0f, 0f), Quaternion.identity);

                    ballInst1.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst1.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;

                    var ballInst2 = Instantiate(ball, new Vector3(0f, -4.5f, 0f), Quaternion.identity);

                    ballInst2.transform.Find("Ball_Behaviour").GetComponent<Ball>().type = Ball.Type.simple_ball;
                    ballInst2.transform.Find("Ball_Behaviour").GetComponent<Ball>().state = Ball.State.initializing;
                }
            }
        }
    }

    bool isClearZoneToSpawn(float posX, float posY)
    {
        Vector2 position = new Vector2(posX, posY);
        Collider2D[] intersecting = Physics2D.OverlapCircleAll(position, 2.5f);

        if (intersecting.Length == 0)
            return true;
        else
            return false;
    }
}
