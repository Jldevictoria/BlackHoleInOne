using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitObject : MonoBehaviour
{
    public GameObject targetBody;
    public float radius = 10.0f;
    public bool clockwise = false;
    public float skew = 0.0f;
    public float orbitSpeed = 1.0f;
    public float startAngle = 0.0f;
    
    private float angle = 0.0f;
    private float direction = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        angle = startAngle;
        if (clockwise) {
            direction = -1.0f;
        }
        else {
            direction = 1.0f;
        }
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        angle += direction * Time.deltaTime * orbitSpeed;
        Vector3 targetPosition = targetBody.transform.position;
        targetPosition.x += Mathf.Sin(angle) * radius;
        targetPosition.y += Mathf.Cos(angle) * radius;
        transform.position = targetPosition;
    }
}
