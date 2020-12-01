using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyBackgroundController : MonoBehaviour
{
    public Camera lobbyCamera;
    public GameObject splotch0;
    public GameObject splotch1;
    public GameObject splotch2;
    private List<GameObject> splotches = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // float cameraOrtho = lobbyCamera.orthographicSize;
        // float hextent = cameraOrtho * Screen.width / Screen.height;
        // float minX = hextent / 2.0f;
        // float maxX = hextent * 2.0f;
        // float minY = cameraOrtho / 2.0f;
        // float maxY = cameraOrtho * 2.0f;
        // UnityEngine.Debug.Log(cameraOrtho);
        // UnityEngine.Debug.Log(hextent);
        // UnityEngine.Debug.Log(minX);
        // UnityEngine.Debug.Log(maxX);
        // UnityEngine.Debug.Log(minY);
        // UnityEngine.Debug.Log(maxY);
        for (int i = 0; i < 10; i++) {
            // float randX = Random.Range(minX, maxX);
            // float randY = Random.Range(minY, maxY);
            // float randSpin = Random.Range(0.0f, 360.0f);
            // UnityEngine.Debug.Log("X: " + randX + " Y: " + randY);
            // Instantiate(splotch0, new Vector3(randX, randY, 0), Quaternion.Euler(0.0f, 0.0f, randSpin));
            SpawnRandom(splotch0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRandom(GameObject o) {
        Vector3 screenPosition = lobbyCamera.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), lobbyCamera.farClipPlane/2));
        splotches.Add(Instantiate(o,screenPosition,Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))));
    }
}
