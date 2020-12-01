using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : AstralBodyController
{
    public GameObject moonBall;
    public LevelController levelController;
    public Collider2D orbitRing;
    private bool ball_destroyed;

    // Start is called before the first frame update
    void Start()
    {
        //mass = 200;
        ball_destroyed = false;

        moonBall = GameObject.Find("MoonBall");
        levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        orbitRing = transform.GetChild(2).gameObject.GetComponent<Collider2D>();
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
                //// Restore ball
                moonBall.SetActive(true);
                // Flip bool
                ball_destroyed = false;
                levelController.OutOfBounds();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

    }

    public void OnChildTriggerEntered(Collider2D other, Vector3 childPosition)
    {
        //Do stuff
        if (other.tag == "Player")
        {
            // Message
            print("Crashed into the star! You take an extra stroke.");
            // Flip Bool
            ball_destroyed = true;
            // Turn off ball
            moonBall.SetActive(false);
        }
    }
}
