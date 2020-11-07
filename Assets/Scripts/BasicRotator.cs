using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRotator : MonoBehaviour
{
    public float rotationSpeed = 10.0f;
    public bool clockwise = false;
    private float direction = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 rotationVector = transform.rotation.eulerAngles;
        rotationVector.z += direction * Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
