using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private ScoreKeeper scoreKeeper;

    //-----------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        ball.Restart(null);
    }

    //-----------------------------------------------------------------------------
    public void Score(Goal goal)
    {
        if(scoreKeeper.AddScore(goal)) ball.Restart(goal);
    }

    public void unScore(Vector3 dir)
    {
        scoreKeeper.RemoveScore(dir);
    }
}
