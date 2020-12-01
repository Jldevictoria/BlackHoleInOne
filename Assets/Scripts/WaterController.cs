using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : AstralBodyController
{
    public GameObject moonBall;
    public LevelController levelController;
    public Collider2D orbitRing;
    public OrbitObject moonBallOrbit;
    private bool ball_destroyed;

    // Start is called before the first frame update
    void Start()
    {
        ball_destroyed = false;

        moonBall = GameObject.Find("MoonBall");
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        orbitRing = transform.GetChild(2).gameObject.GetComponent<Collider2D>();
        moonBallOrbit = moonBall.GetComponent<OrbitObject>();
    }

    void FixedUpdate()
    {
        // Stop ball
        if (ball_destroyed)
        {
            moonBall.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ball_destroyed)
        {
            // Wait for click
            if (Input.GetMouseButton(0))
            {
                // Restore ball
                moonBall.SetActive(true);
                moonBall.transform.position = new Vector3(transform.position.x + 10, transform.position.y + 10);
                // Orbit Ring
                moonBallOrbit.changeTargetBody(this.gameObject, 1.0f, 5);
                // Flip bool
                ball_destroyed = false;
            }
        }
    }

    public void OnChildTriggerEntered(Collider2D other, Vector3 childPosition)
    {
        //Do stuff
        if (other.tag == "Player")
        {
            // Message
            print("Ploof! Take an extra stroke.");
            // Flip Bool
            ball_destroyed = true;
            // Turn off ball
            moonBall.SetActive(false);
        }
    }
}
