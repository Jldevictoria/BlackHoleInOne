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
    public float direction = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate angle and radius based on positions in scene - lets us drag and drop the planets and satellites
        Vector3 targetPosition = targetBody.transform.position;
        radius = Vector3.Distance(targetPosition, transform.position);
        angle = getAngle(targetPosition, transform.position, radius);

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
        angle += direction * Time.deltaTime * orbitSpeed;
        Vector3 targetPosition = targetBody.transform.position;
        targetPosition.x += Mathf.Cos(angle) * radius;
        targetPosition.y += Mathf.Sin(angle) * radius;
        transform.position = targetPosition;
    }

    // getAngle finds the starting angle of the satellite with respect to the orbit it creates
    public float getAngle(Vector3 center, Vector3 satellite, float _radius)
    {
        Vector3 relativePosition = satellite - center;
        float unitAngle = Mathf.Acos(relativePosition.x/_radius) * Mathf.Sign(relativePosition.y);
        return unitAngle;
    }

    public void changeTargetBody(GameObject newBody, float rotation, float new_radius)
    {
        targetBody = newBody;
        direction = rotation;
        radius = new_radius;
        Start();
    }
}
