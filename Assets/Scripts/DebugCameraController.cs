using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraController : MonoBehaviour
{
    public float speed = 5.0f;
    public float cameraZoomSpeed = 1.0f;
    public float cameraPanSpeed = 0.1f;
    public float cameraZoomMax = 100;
    public float cameraZoomMin = 5;
    public GameObject moonBall;

    private Vector3 previousMousePosition;
    private bool captureMouseMovement;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(moonBall.transform.position.x + 5, moonBall.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Handle zooming camera with scroll wheel.
        if ((Input.GetAxis("Mouse ScrollWheel") > 0) && (Camera.main.orthographicSize >= cameraZoomMin)) {
            Camera.main.orthographicSize -= cameraZoomSpeed;
        }
        else if ((Input.GetAxis("Mouse ScrollWheel") < 0) && (Camera.main.orthographicSize <= cameraZoomMax)) {
            Camera.main.orthographicSize += cameraZoomSpeed;
        }

        // Handle panning camera with mouse click and drag.
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider == null) {
                previousMousePosition = Input.mousePosition;
                captureMouseMovement = true;
            }
        }
        if (Input.mousePosition != previousMousePosition && captureMouseMovement) {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePosition;
            previousMousePosition = Input.mousePosition;
            transform.position -= deltaMousePosition * cameraPanSpeed;
        }
        if (Input.GetMouseButtonUp(0) && captureMouseMovement) {
            captureMouseMovement = false;
        }

        if (moonBall.GetComponent<Rigidbody2D>().velocity != Vector2.zero) {
            transform.position = new Vector3(moonBall.transform.position.x, moonBall.transform.position.y, transform.position.z);
        }
    }
}
