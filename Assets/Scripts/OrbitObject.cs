using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitObject : MonoBehaviour
{
    public GameObject targetBody;
    public bool clockwise = false;
    public float skew = 0.0f;
    public float orbitSpeed = 1.0f;
    public float startAngle = 0.0f;
    public float radius = 10.0f;    
    public float angle = 0.0f;
    public float sinkRatio = 1.0f;
    public float shrinkRatio = 1.0f;
    public bool isSinking = false;

    private float direction = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log(targetBody);
        if (targetBody != null) {
            // Calculate angle and radius based on positions in scene - lets us drag and drop the planets and satellites
            Vector3 targetPosition = targetBody.transform.position;
            radius = Vector3.Distance(targetPosition, transform.position);
            angle = getAngle(targetPosition, transform.position);
        }
        // Sets rotation direction
        if (clockwise) {
            direction = -1.0f;
        }
        else {
            direction = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetBody != null) {
            angle += direction * Time.deltaTime * orbitSpeed;
            Vector3 targetPosition = targetBody.transform.position;
            targetPosition.x = Mathf.Cos(angle) * radius;
            targetPosition.y = Mathf.Sin(angle) * radius;
            if (targetPosition.x != float.NaN && targetPosition.y != float.NaN) {
                transform.position = targetPosition;
            }
            if (targetBody.tag == "BlackHole" && isSinking) {
                if (radius > 0) {
                    radius *= sinkRatio;
                    sinkRatio -= 0.000001f;
                }
                if (transform.localScale.x > 0 || transform.localScale.y > 0) {
                    float shrinkX = transform.localScale.x * shrinkRatio;
                    float shrinkY = transform.localScale.x * shrinkRatio;
                    Vector3 shrinkVector = new Vector3(shrinkX, shrinkY, transform.localScale.z);
                    transform.localScale = shrinkVector;
                }
                orbitSpeed *= 1.0f/shrinkRatio;
                GetComponent<Rigidbody2D>().angularVelocity -= 0.5f;
            }
        }
    }

    // getAngle finds the starting angle of the satellite with respect to the orbit it creates
    private float getAngle(Vector3 center, Vector3 satellite)
    {
        Vector3 relativePosition = satellite - center;
        float unitAngle = Mathf.Acos(relativePosition.x/radius) * Mathf.Sign(relativePosition.y);
        return unitAngle;
    }

    public void changeTargetBody(GameObject newBody, bool clockwise)
    {
        targetBody = newBody;
        this.clockwise = clockwise;
        Start();
    }
}
