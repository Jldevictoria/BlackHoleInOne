using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBallController : MonoBehaviour
{
    public float power;

    private Rigidbody2D rigidBody2D;
    private LineRenderer lineRenderer;
    private GameObject line;
    private Vector3 startMousePosition;
    private bool captureMouseMovement;
    private bool canLaunch;
    private OrbitObject orbiting;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        orbiting = GetComponent<OrbitObject>();
        canLaunch = true;
        captureMouseMovement = false;
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
            // canLaunch = false; // Turning this off right now because its fun
            // Make current planet non-interactable

        }
        else
        {
            // Resume movement
            UnfreezeGame();
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
        print("I am triggered.");

        // Stop moving
        rigidBody2D.velocity = new Vector2(0, 0);

        // Orbit around new object
        orbiting.enabled = true;
        float rotation = -1.0f; // TODO: Make based on collision position
        orbiting.changeTargetBody(other.gameObject, rotation);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        print("I am no longer triggered.");
    }
}
