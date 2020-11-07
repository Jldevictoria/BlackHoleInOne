using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraController : MonoBehaviour
{
    public float speed = 5.0f;
    public float cameraZoomSpeed = 1.0f;
    public float cameraPanSpeed = 0.1f;

    private Vector3 previousMousePosition;
    private bool captureMouseMovement;
    // Update is called once per frame
    void Update()
    {
        //Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0.0f);
        // Handle zooming camera with scroll wheel.
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            Camera.main.orthographicSize -= cameraZoomSpeed;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            Camera.main.orthographicSize += cameraZoomSpeed;
        }
        // Handle panning camera with mouse click and drag.
        if (Input.GetMouseButtonDown(0)) {
            previousMousePosition = Input.mousePosition;
            captureMouseMovement = true;
        }
        if (Input.mousePosition != previousMousePosition && captureMouseMovement) {
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePosition;
            previousMousePosition = Input.mousePosition;
            transform.position -= deltaMousePosition * cameraPanSpeed;
        }
        if (Input.GetMouseButtonUp(0)) {
            captureMouseMovement = false;
        }
    }
}
