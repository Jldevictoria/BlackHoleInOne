using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBallController : MonoBehaviour
{
    public float power;

    // Gravity well business
    public float gravCoeff;
    public Dictionary<string, Collider2D> planetArray;


    public Rigidbody2D rigidBody2D;
    public LineRenderer lineRenderer;
    public GameObject line;
    public Vector3 startMousePosition;
    public bool captureMouseMovement;
    public bool canLaunch;
    public OrbitObject orbiting;
    public bool canGravity;
    public GameObject levelControllerObject;
    public string[] planetNames;
    private LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        levelController = levelControllerObject.GetComponent<LevelController>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        orbiting = GetComponent<OrbitObject>();
        canLaunch = true;
        captureMouseMovement = false;
        canGravity = false;
        // Check gravity well status
        checkGravity();
    }

    // Update is called once per frame
    void Update()
    {
        // Handle panning camera with mouse click and drag.
        if (Input.GetMouseButtonDown(0) && canLaunch) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null) {
                if (hit.collider.gameObject == gameObject) {
                    UnityEngine.Debug.Log("Player clicked on " + gameObject.name);
                    startMousePosition = transform.position;
                    captureMouseMovement = true;
                    // Update stroke count
                    levelController.UpdateLevelScore();

                    // Save current state so we can reload
                    print("TODO: Game state should save here.");
                    levelController.SaveState();
                }
            }
        }
        if (Input.GetMouseButton(0) && captureMouseMovement) {
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, new Vector3(startMousePosition.x, startMousePosition.y, 0.0f));
            lineRenderer.SetPosition(1, new Vector3(currentMousePos.x, currentMousePos.y, 0.0f));
        }
        if (Input.GetMouseButtonUp(0) && captureMouseMovement) {
            Vector3 delta = startMousePosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            rigidBody2D.velocity = new Vector2((delta.x * power), (delta.y * power));
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
            captureMouseMovement = false;
        }

        // Freeze while holding down mouseclick
        if (captureMouseMovement)
        {
            // Freeze all movements
            FreezeGame();
            // Free the moon from its current planet
            orbiting.enabled = false;
            canGravity = true;
            canLaunch = false; // Turning this off right now because its fun
            // Make current planet non-interactable

        }
        else
        {
            // Resume movement
            UnfreezeGame();
        }

        // Gravity Well Handling
        // Check if planet array is empty
        if (planetArray.Count == 0)
        {
            // Do nothing
        }
        // Else
        else if(canGravity == true)
        {
            // Set up total force vector
            Vector3 finalForce = Vector3.zero;
            
            // for each planet
            foreach (KeyValuePair<string, Collider2D> entry in planetArray)
            {
                // do something with entry.Value or entry.Key
                // Calculate radius
                float forceRadius = Vector3.Distance(entry.Value.transform.position, transform.position);
                // Calculate direction between moon and current object
                Vector3 forceVector = entry.Value.transform.position - transform.position;
                Vector3 forceDirection = forceVector / forceRadius;
                // Get mass from Astral Body Controller
                float mass = entry.Value.transform.parent.gameObject.GetComponent<AstralBodyController>().mass;
                //print(mass);
                // Calculate force to apply
                finalForce += forceDirection * gravCoeff * mass / Mathf.Pow(forceRadius, 2);
                print(entry.Key);
            }
            // Add total force
            rigidBody2D.AddForce(finalForce);
        }
        else
        {
            // do nothing
        }
    }

    void FreezeGame()
    {
        Time.timeScale = 0;
    }

    void UnfreezeGame()
    {
        Time.timeScale = 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.transform.parent.gameObject.name);
        //print("I am triggered.");
        if (other.tag == "gravityWellEnd")
        {
            if (planetArray.ContainsKey(other.transform.parent.gameObject.name))
            {
                planetArray[other.transform.parent.gameObject.name] = other;
            }
            else
            {
                planetArray.Add(other.transform.parent.gameObject.name, other);
            }
        }
        else if(other.tag == "orbitRing")
        {
            // Stop moving
            rigidBody2D.velocity = new Vector2(0, 0);
            canGravity = false;
            canLaunch = true;

            // Orbit around new object
            orbiting.enabled = true;

            // TODO: Make based on collision position
            float rotation = -1.0f; 
            orbiting.changeTargetBody(other.transform.parent.gameObject, rotation);

            // Remove from gravity list
            planetArray.Remove(other.transform.parent.gameObject.name);
        }
        else
        {
            // do nothing
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //print(other.transform.parent.gameObject.name);

        if (other.tag == "gravityWellStart")
        {
            // If planet already not in orbit
            if (!orbiting.enabled)
            {
                if (planetArray.ContainsKey(other.transform.parent.gameObject.name))
                {
                    planetArray[other.transform.parent.gameObject.name] = other;
                }
                else
                {
                    planetArray.Add(other.transform.parent.gameObject.name, other);
                }
                //print("Leaving " + other.transform.parent.gameObject.name + " well.");
            }
            // if in orbit
            else
            {
                // do nothing
            }
        }
        else if (other.tag == "gravityWellEnd")
        {
            planetArray.Remove(other.transform.parent.gameObject.name);
            //print("Leaving " + other.transform.parent.gameObject.name + " well.");
        }
        else if (other.tag == "orbitRing")
        {
            // do nothing
        }
        else if (other.tag == "boundary")
        {
            print("TODO: Implement out of bounds logic.");
            levelController.OutOfBounds();
            // TODO: Clean this garbage
            rigidBody2D.velocity = new Vector2(0, 0);
            Start();
        }
        else
        {
            // do nothing

        }
    }

    public void checkGravity()
    {
        planetArray = new Dictionary<string, Collider2D>();
        GameObject[] gravityWells; 
        gravityWells = GameObject.FindGameObjectsWithTag("gravityWellEnd");

        foreach (GameObject gravityWell in gravityWells)
        {
            Collider2D collider = gravityWell.GetComponent<Collider2D>();

            // if position is within gravityWell
            if (collider.bounds.Contains(transform.position))
            {
                if (planetArray.ContainsKey(gravityWell.transform.parent.gameObject.name))
                {
                    planetArray[gravityWell.transform.parent.gameObject.name] = collider;
                }
                else
                {
                    planetArray.Add(gravityWell.transform.parent.gameObject.name, collider);
                }
            }
            // Else
            else
            {
                // do nothing
            }
        }
    }

}
