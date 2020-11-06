﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraController : MonoBehaviour
{
    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed, 0.0f);
        UnityEngine.Debug.Log(transform.position);
        transform.position = transform.position + movement * Time.deltaTime;
    }
}
