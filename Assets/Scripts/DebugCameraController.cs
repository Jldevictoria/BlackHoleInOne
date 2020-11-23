using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraController : MonoBehaviour
{
    public float cameraZoomSpeed = 25.0f;
    public float cameraPanSpeed = 0.07f;
    public float cameraSmoothSpeed = 30.0f;
    public float cameraZoomMax = 50.0f;
    public float cameraZoomMin = 5.0f;
    public GameObject moonBall;

    private Vector3 previousMousePosition;
    private bool captureMouseMovement;
    private float targetOrtho;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(moonBall.transform.position.x, moonBall.transform.position.y, transform.position.z);
        targetOrtho = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle zooming camera with scroll wheel.
        float scroll = Input.GetAxis ("Mouse ScrollWheel");
        if (scroll != 0.0f) {
            targetOrtho -= scroll * cameraZoomSpeed;
            targetOrtho = Mathf.Clamp (targetOrtho, cameraZoomMin, cameraZoomMax);
        }

        if ((Camera.main.orthographicSize >= cameraZoomMin) &&
            (Camera.main.orthographicSize <= cameraZoomMax) &&
            (Camera.main.orthographicSize != targetOrtho)) {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, cameraSmoothSpeed * Time.deltaTime);
        }
        else if (Camera.main.orthographicSize < cameraZoomMin) {
            Camera.main.orthographicSize = cameraZoomMin;
        }
        else if (Camera.main.orthographicSize > cameraZoomMax) {
            Camera.main.orthographicSize = cameraZoomMax;
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
