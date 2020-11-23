using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public int levelScore;
    public int levelPar;

    public GameObject startPlanet;
    public GameObject endPlanet;
    public GameObject moonBall;

    public GameObject curPlanet;
    public GameObject boundary;

    public Dictionary<GameObject, Vector3> curState;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        levelScore = 0;
        startPlanet = moonBall.GetComponent<OrbitObject>().targetBody;
        curPlanet = startPlanet;
    }

    // Update is called once per frame
    void Update()
    {
        curPlanet = moonBall.GetComponent<OrbitObject>().targetBody;

        if (curPlanet == endPlanet)
        {
            print("You delivered the moon in " + levelScore + " shots!");
            if (levelScore < levelPar)
            {
                print("You were under par (" + levelPar + ")!");
            }
            else if (levelScore > levelPar)
            {
                print("You were over par (" + levelPar + ")!");
            }
            else if (levelScore == levelPar)
            {
                print("You were at par (" + levelPar + ")!");
            }
            else
            {
                // do nothing
            }
            gameController.NextLevel(levelScore);
        }
    }

    public void UpdateLevelScore()
    {
        levelScore += 1;
    }

    public void SaveState()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject astralBody in allObjects)
        {
            curState.Add(astralBody, astralBody.transform.position);
            print(astralBody.name);
        }
    }

    public void LoadState()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject astralBody in allObjects)
        {
            astralBody.transform.position = curState[astralBody];
        }
        // Clear dict
        curState = new Dictionary<GameObject, Vector3>();
    }

}
