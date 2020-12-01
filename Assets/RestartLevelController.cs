using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelController : MonoBehaviour
{
    public GameObject gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
