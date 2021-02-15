using UnityEngine;
using UnityEngine.Serialization;

public class Ball : MonoBehaviour {
    [SerializeField] private Goal leftGoal;
    [SerializeField] private Goal rightGoal;

    public float startSpeed;
    public float step;
    public bool useDebugVisualization;
    public bool startShot;

    private float speed;
    private Rigidbody rb;

    //-----------------------------------------------------------------------------
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        speed = startSpeed;
    }

    //-----------------------------------------------------------------------------
    // Update is called once per frame
    public void Restart(Goal goal)
    {
        speed = startSpeed;
        rb.MovePosition(Vector3.zero);

        startShot = true;
        changeColor(Color.white);

        if(goal == leftGoal) gameObject.GetComponent<Rigidbody>().velocity = Vector3.left * speed;
        else gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * speed; // possibly randomize starting ball: if(goal == rightGoal)
        // else gameObject.GetComponent<Rigidbody>().velocity = Vector3.right * speed; // change to send to losing side
    }

    //-----------------------------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PaddleLeft" || collision.gameObject.name == "PaddleRight")
        {
            //play sound
            collision.gameObject.GetComponent<Paddle>().MadeContact(speed);

            speed += step;
            float heightAboveOrBelow = transform.position.z - collision.transform.position.z;
            float maxHeight = collision.collider.bounds.extents.z;
            float percentOfMax = heightAboveOrBelow / maxHeight;


            if (useDebugVisualization) {
                DebugDraw.DrawSphere(transform.position, 0.5f, Color.green);
                DebugDraw.DrawSphere(collision.transform.position, 0.5f, Color.red);
                Debug.Break();
                Debug.Log($"percent height = {percentOfMax}");
            }

            // refresh color
            changeColor(Color.white);

            startShot = false;

            bool hitLeftPaddle = collision.gameObject.name == "PaddleLeft";
            float newHorizontalSpeed = (hitLeftPaddle) ? speed: -speed;

            Vector3 newVelocity = new Vector3(newHorizontalSpeed, 0f, percentOfMax * 4f).normalized * speed;
            rb.velocity = newVelocity;
        }
    }

    public void changeColor(Color color){
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
    }
}
