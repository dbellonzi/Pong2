using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Ball gameBall;
    [SerializeField] private Paddle leftPaddle;
    [SerializeField] private Paddle rightPaddle;
    [SerializeField] private GameManager gameManager;

    private Rigidbody rb;
    public Vector3 pos;

    void Start()
    {
        setPos();
    }

    //-----------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        //do something interesting to the ball, paddle, or some other game element
        
        gameBall = other.GetComponent<Ball>(); 
        // on contact with the ball make the ball clear       
        if(this.name == "InvisiBall")
        {    
            if(gameBall) gameBall.changeColor(Color.clear);
        }
        if(this.name == "Loss" && !gameBall.startShot)
        {
            rb = other.GetComponent<Rigidbody>();
            gameManager.unScore(rb.velocity);
        }

        // reset position of the powerup
        setPos();
    }

    //--------------------------  Reset Ball Position  ------------------------------
    void setPos()
    {
        // pick random position on game plane and move powerup there
        pos = new Vector3(Random.Range(-8.0f, 8.0f), 0.5f, Random.Range(-7.0f, 7.0f));
        this.transform.position = pos;
    }
}
