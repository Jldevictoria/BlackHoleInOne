using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildColliderWater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        transform.parent.gameObject.GetComponent<WaterController>().OnChildTriggerEntered(other, transform.position);
    }
}
