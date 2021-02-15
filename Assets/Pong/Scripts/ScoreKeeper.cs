using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private Text leftTextScore;
    [SerializeField] private Text rightTextScore;
    [SerializeField] private Text gameOverLabel;
    [SerializeField] private Text winnerLabel;

    [SerializeField] private Goal leftGoal;
    [SerializeField] private Goal rightGoal;

    [SerializeField] private GameManager gameManager;


    private int leftScore = 0;
    private int rightScore = 0;
    private string gameOver = "GAME OVER";
    private string winner;

    public int MAXSCORE = 11;
    // Start is called before the first frame update
    void Start()
    {
        RefreshScore();
    }

    private void RefreshScore()
    {
        // if game over
        if(leftScore==MAXSCORE||rightScore==MAXSCORE){
            
            // check which side won
            if(leftScore==MAXSCORE) winner = "Left Player Wins";
            else winner = "Right Player Wins";

            // update game over label
            gameOverLabel.text = gameOver;
            winnerLabel.text = winner;
            leftTextScore.text = rightTextScore.text = " ";

        } else {

            // update left side score
            float scoreModL = (float)leftScore/MAXSCORE;
            leftTextScore.color = new Color (0, 0, scoreModL);
            leftTextScore.text = (leftScore.ToString("D2"));
            if(leftScore < MAXSCORE/2) leftTextScore.color = Color.white;

            // update right side score
            float scoreModR = (float)rightScore/MAXSCORE;
            rightTextScore.color = new Color (scoreModR, 0, 0);
            rightTextScore.text = (rightScore.ToString("D2"));
            if(rightScore < MAXSCORE/2) rightTextScore.color = Color.white;

        }
    }

    public bool AddScore(Goal scoringSide)
    {
        // update score
        if (scoringSide == leftGoal)rightScore += 1;
        else leftScore += 1;

        // update score board
        RefreshScore();

        // if game isn't over return true
        if(leftScore == MAXSCORE || rightScore == MAXSCORE)return false;
        else return true;

    }

    public void RemoveScore(Vector3 dir)
    {
        // update score
        if(dir.x < 0 && rightScore > 0) rightScore -= 1;
        else if(dir.x > 0 && leftScore > 0) leftScore -= 1;
        else return;

        RefreshScore(); 
    }
}
